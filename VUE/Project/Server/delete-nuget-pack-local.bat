@echo off
set target_file_path="./ManagementPlatform.Production/src/ManagementPlatform.Production.Domain.Shared/bin/Debug/ManagementPlatform.Production.Domain.Shared.0.1.0.nupkg"
if exist %target_file_path% (
    del /F %target_file_path%
    echo 文件已成功删除。
) else (
    echo 文件不存在。
)
