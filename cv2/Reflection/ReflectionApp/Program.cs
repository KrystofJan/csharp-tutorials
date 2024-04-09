

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;




namespace ReflectionApp
{
    public class Program
    {
        public static void Main(string[] args) {
            string path = Path.GetFullPath("../../../../MyLib/bin/Debug/net6.0/MyLib.dll");
            Assembly assembly = Assembly.LoadFile(path);
            Type[] types = assembly.GetTypes();
            
            // object obj = assembly.CreateInstance("MyLib.Controllers.CustomerController");
            Type controllerType = assembly.GetType("MyLib.Controllers.CustomerController");

            object obj = Activator.CreateInstance(controllerType);
            
            MethodInfo listMethod = controllerType.GetMethod("List");
            
            string result = (string) listMethod.Invoke(obj, new object[] {2});
            
            Console.WriteLine(result);


            // string url = "/Customer/List?limit=3";
            string url = "/Customer/Add?Name=Pepa&Age=30&IsActive=true";

            string[] urlParts = url.Split('?');
            
            string urlObjectRaw = urlParts[0];
            string[] urlObject = urlObjectRaw.Split('/');
            string urlController = urlObject[1];
            string urlMethod= urlObject[2];
            
            
            string urlParamsRaw = urlParts[1];
            string[] urlParams = urlParamsRaw.Split('&');


            Dictionary<string, string> urlParamsList = urlParams.Select(x => x.Split('='))
                                                                .ToDictionary(x=>x[0], x=> x[1], StringComparer.OrdinalIgnoreCase);

            string classFullName = $"MyLib.Controllers.{urlController}Controller";
            Type classType = assembly.GetType(classFullName);
            
            if (classType == null) {
                Console.WriteLine("404");
                return;
            }

            object objRes = Activator.CreateInstance(classType);
            
            MethodInfo classMethod = classType.GetMethod(urlMethod);

            if (classMethod == null) {
                Console.WriteLine("500");
                return;
            }

            List<object> methodArgs = new List<object>();

            foreach (ParameterInfo param in classMethod.GetParameters()) {
                if (param.ParameterType == typeof(int)) {
                    methodArgs.Add(int.Parse(urlParamsList[param.Name]));
                } 
                else if (param.ParameterType == typeof(bool)) {
                    methodArgs.Add(bool.Parse(urlParamsList[param.Name]));
                } 
                else if (param.ParameterType == typeof(string)) {
                    methodArgs.Add(Convert.ToString(urlParamsList[param.Name]));
                }
                else {
                    throw new Exception("sdas");
                }
            }
            string methodResult = (string)classMethod.Invoke(objRes, methodArgs.ToArray());
            
            Console.WriteLine(methodResult);
            
            
            
        }

    }
}