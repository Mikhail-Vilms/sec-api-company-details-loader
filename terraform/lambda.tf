data "archive_file" "lambda_zip_payload" {
    type = "zip"
    source_dir = "../${local.lambdaProjectName}/bin/Debug/netcoreapp3.1/"
    output_path = "lambda_payload.zip"
}

resource "aws_lambda_function" "loader-function" {
    filename = data.archive_file.lambda_zip_payload.output_path
    function_name = locals.lambdaName
    handler = "${local.lambdaProjectName}::${local.lambdaProjectName}.Function::FunctionHandler"
    runtime = "dotnetcore3.1"
    role = aws_iam_role.sec-api-company-details-loader-lambda-exec-role.arn
    source_code_hash = filebase64sha256("lambda_payload.zip")
    publish = "true"
    timeout = 30
}

resource "aws_cloudwatch_log_group" "sec-api-company-details-loader-log-group" {
    name = "/aws/lambda/${aws_lambda_function.loader-function.function_name}"
    retention_in_days = 0
    lifecycle {
      prevent_destroy = false
    }
}

resource "aws_cloudwatch_event_rule" "loader-function-invocation-rule" {
  name = "Every-Month-Invocation-Rule"
  description = "Fires every month to updoad information about companies from SEC API"
  schedule_expression = "0 15 10 L * ?" ## Fire at 10:15 AM on the last day of every month
  is_enabled = true
}

resource "aws_lambda_permission" "allow-cloudwatch-to-call-lambda" {
  statement_id = "AlowExecutionFromCloudWatch"
  action = "lambda:InvokeFunction"
  function_name = aws_lambda_function.loader-function.arn
  principal = "events.amazonaws.com"
  source_arn = aws_cloudwatch_event_rule.loader-function-invocation-rule.arn
  depends_on = [
    aws_lambda_function.loader-function,
    aws_cloudwatch_event_rule.loader-function-invocation-rule
  ]
}

resource "aws_cloudwatch_event_target" "load_every_month" {
  rule = aws_cloudwatch_event_rule.loader-function-invocation-rule.name
  target_id = "lambda"
  arn = aws_lambda_function.loader-function.arn
}
