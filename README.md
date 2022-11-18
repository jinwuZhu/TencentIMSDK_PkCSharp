### IM C接口的C#封装

# 环境准备
下载IMSDK，经IM CSDK的DLL解压放到您的项目中
# 使用
将Tencent.cs文件拷贝添加到您的项目中
# 验证效果
```C#
using System;
using Tencent;
namespace HelloWorldApplication
{
   class HelloWorld
   {
      static void Main(string[] args)
      {
         Console.WriteLine("IMVer:{0}",Tencent.TIMGetSDKVersionCSharp());
         Console.ReadKey();
      }
   }
}
```
