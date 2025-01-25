
# 简介
用于自定义文件时间属性，实现文件属性获取与自定义创建、修改、访问时间

还可以与同步指定文件的时间属性

# 帮助
```powershell
SharpFileTimeSetter.exe --help                                                                 
Usage: SharpSharpFileTimeSetter --file <path> [--get] [--creation <date>] [--modification <date>] [--access <date>]
       SharpFileTimeSetter --sync <sourceFile> <targetFile>
Example:
  SharpFileTimeSetter --file test.txt --creation "2025-01-01 12:00:00" --modification "2025-01-02 14:00:00"
  SharpFileTimeSetter --file test.txt --get
  SharpFileTimeSetter --sync source.txt target.txt

Arguments:
  --file          Path to the target file (required).
  --get           Get the current creation, modification, and access time of the file.
  --creation      Creation time to set (optional, format: yyyy-MM-dd HH:mm:ss).
  --modification  Modification time to set (optional, format: yyyy-MM-dd HH:mm:ss).
  --access        Access time to set (optional, format: yyyy-MM-dd HH:mm:ss).
  --sync          Synchronize time properties from source file to target file.
  <sourceFile>    Source file whose time will be copied.
  <targetFile>    Target file whose time will be updated.
```
## 例子

创建测试文件

```powershell
echo test > test
```

获取文件时间属性

```powershell
SharpFileTimeSetter.exe --file test --get
File: test
Creation Time: 2025/1/25 13:17:58
Modification Time: 2025/1/25 13:17:58
Access Time: 2025/1/25 13:17:58
```

修改文件属性

```powershell
SharpFileTimeSetter.exe --file test  --creation "1999-09-09 12:00:00" --modification "1999-09-09 12:00:00" --access "1999-09-09 12:00:00"
Creation time set to 1999/9/9 12:00:00.
Modification time set to 1999/9/9 12:00:00.
Access time set to 1999/9/9 12:00:00.
File time properties updated successfully.
```

检查修改后的属性

![image-20250125132032889](Images/img1.png)

同步指定文件的时间属性

```powershell
# 创建测试文件
PS D:\bin\Debug> echo test2 > test2
PS D:\bin\Debug> .\SharpFileTimeSetter.exe --file test2 --get
File: test2
Creation Time: 2025/1/25 13:22:20
Modification Time: 2025/1/25 13:22:20
Access Time: 2025/1/25 13:22:20

# 将文件 test 的时间属性同步给 test2
PS D:\bin\Debug> .\SharpFileTimeSetter.exe --sync test test2 
Synced times from source 'test' to target 'test2'.
PS D:\bin\Debug> .\SharpFileTimeSetter.exe --file test2 --get
File: test2
Creation Time: 1999/9/9 12:00:00
Modification Time: 1999/9/9 12:00:00
Access Time: 2025/1/25 13:19:54
```

