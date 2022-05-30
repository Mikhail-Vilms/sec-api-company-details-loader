data "archive_file" "lambda_zip_payload" {
    type = "zip"
    source_dir = "../${local.lambdaProjectName}/bin/Debug/netcoreapp3.1/"
    output_path = "lambda_payload.zip"
}

resource "aws_lambda_function" "loader-lambda-function" {
    filename = data.archive_file.lambda_zip_payload.output_path
    function_name = local.lambdaName
    handler = "${local.lambdaProjectName}::${local.lambdaProjectName}.Function::FunctionHandler"
    runtime = "dotnetcore3.1"
    role = aws_iam_role.loader-lambda-function-exec-role.arn
    source_code_hash = filebase64sha256("lambda_payload.zip")
    publish = "true"
    timeout = 300
}

resource "aws_cloudwatch_log_group" "sec-api-company-details-loader-log-group" {
    name = "/aws/lambda/${aws_lambda_function.loader-lambda-function.function_name}"
    retention_in_days = 0
    lifecycle {
      prevent_destroy = false
    }
}

resource "aws_cloudwatch_event_rule" "loader-function-invocation-rule" {
  name = "Every-Month-Invocation-Rule"
  description = "Fires every month to updoad information about companies from SEC API"
  ## https://docs.aws.amazon.com/AmazonCloudWatch/latest/events/ScheduledEvents.html
  schedule_expression = "cron(0 8 1 * ? *)" ## Run at 8:00 am (UTC) every 1st day of the month
  is_enabled = true
}

resource "aws_lambda_permission" "allow-cloudwatch-to-call-lambda" {
  statement_id = "AlowExecutionFromCloudWatch"
  action = "lambda:InvokeFunction"
  function_name = aws_lambda_function.loader-lambda-function.arn
  principal = "events.amazonaws.com"
  source_arn = aws_cloudwatch_event_rule.loader-function-invocation-rule.arn
  depends_on = [
    aws_lambda_function.loader-lambda-function,
    aws_cloudwatch_event_rule.loader-function-invocation-rule
  ]
}

resource "aws_cloudwatch_event_target" "load_every_month" {
  rule = aws_cloudwatch_event_rule.loader-function-invocation-rule.name
  target_id = "lambda"
  arn = aws_lambda_function.loader-lambda-function.arn
}
