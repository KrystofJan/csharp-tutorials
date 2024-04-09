using System;

using System.Text.RegularExpressions;
using System.Xml;

namespace ConsoleApp2 // Note: actual namespace depends on the project name.
{
	internal partial class Program {


		static void Main(string[] args) {
			// xDoc.CreateXmlDeclaration(
			// 	"1.0",
			// 	"UTF-8",
			// 	"yes"
			// );
			//
			// XmlNode root = xDoc.CreateElement("Root");
			// xDoc.AppendChild(root);
			//
			// XmlNode toor = xDoc.CreateElement("Toor");
			// root.AppendChild(toor);
			//
			// XmlNode textNode = xDoc.CreateTextNode("Text uvnitr uzlu");
			// toor.AppendChild(textNode);
			//
			// XmlNode toor2 = xDoc.CreateElement("Thor");
			// root.AppendChild(toor2);
			//
			//
			// XmlAttribute childAttr = xDoc.CreateAttribute("id");
			// childAttr.Value = "420";
			//
			// toor2.Attributes.Append(childAttr);
			//
			// xDoc.Save("test.xml");

			
			// XmlDocument xDoc = new XmlDocument();
			// xDoc.Load("text.xml");
			// Console.WriteLine(xDoc.DocumentElement.Name);
			// Console.WriteLine(xDoc.DocumentElement.ChildNodes[0].Name);
			// Console.WriteLine(xDoc.DocumentElement.ChildNodes[0].ChildNodes[0].Value);
			//
			// xDoc.DocumentElement.RemoveChild(xDoc.DocumentElement.ChildNodes[1]);
			//
			//
			// xDoc.Save("text2.xml");
			
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load("lupa.xml");
			// foreach (XmlNode node in xDoc.SelectNodes("rss/channel/item/title/text()")) { // () vrati textNode
			// foreach (XmlNode node in xDoc.SelectNodes("//title/text()")) { // () vrati vsechcny title
			foreach (XmlNode node in xDoc.SelectNodes("rss/channel/item")) { // () vrati textNode
				Console.WriteLine(node.SelectNodes("title/text()"));
				Console.WriteLine(node.SelectNodes("pubDate/text()")
				);
			}
			
			
			
            
		}
		
		
		[GeneratedRegex(@"\{([a-zA-Z]+)\}", RegexOptions.IgnoreCase)]
		private static partial Regex test();
		static void Regex() {
			Dictionary<string, string> dict = new Dictionary<string, string> {
				{"name", "Petr"},
				{"orderName", "Auto"},
				{"price", "444434234234"}
			};
			
			
			Regex regex = new Regex(@"^[\w\d_\-\.]+@[\w\d_\-\.]+\.[a-z]{2,6}$");
			bool isMatch = regex.IsMatch("zah0089@vsb.cz");
			
			Console.WriteLine(isMatch);
			
			// regex

			Regex parserRegex = new Regex(@"(^https?:\/\/)(([\w\d]+)\.)?([\w\d]+)\.([\w]{2,6})");
			string[] urls = new string[] {
				"https://katedrainformatiky.cz/cs/pro-uchazece/zamereni-studia",
				"http://katedrainformatiky.cz/",
				"https://katedrainformatiky.cz?page=5",
				"https://page.katedrainformatiky.cz?url=http://test.cz/"
			};

			foreach (string url in urls) {
				Match match = parserRegex.Match(url);

				if (!match.Success) {
					Console.WriteLine($"{url} - neni url");
					continue;
				}
				
				string g1 = match.Groups[1].Value;
				Console.WriteLine(url);
				Console.WriteLine($"-{match.Groups[0].Value}");
				Console.WriteLine($"-{match.Groups[1].Value}");
				Console.WriteLine($"-{match.Groups[2].Value}");
				Console.WriteLine($"-{match.Groups[3].Value}");
				Console.WriteLine($"-{match.Groups[4].Value}\n");
			}

			string text = "Ahoj {name}. Tvá objednávka „{orderName}“ v ceně {price} byla úspěšně uhrazena.";

			Regex textRegex = test();

			string finalTXT = textRegex.Replace(text, (Match match) => {
				string key = match.Groups[1].Value;
				if (dict.TryGetValue(key, out string val)) {
					return val;
				}
				return key.ToUpper();
			});
			Console.WriteLine(finalTXT);
		}
	}
}