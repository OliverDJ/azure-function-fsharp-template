Param(
    [string]$key="ConnectionStrings:InsertDatabase",
    [string]$value="Server=localhost, 1111; Database=InsertDatabase; User ID=SA; Password=password"
)

[Environment]::SetEnvironmentVariable($key, $value)