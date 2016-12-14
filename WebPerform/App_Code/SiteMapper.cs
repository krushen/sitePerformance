using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPerform.App_Code
{
    public static class SiteMapper
    {
        public static List<string> BuildSiteMap(string url, List<string> urlArray)
        {
            for (int i = 0; i < urlArray.Count; i++)
            {
                string item = urlArray[i];
                if (item.Length > 5 && item[0] == '/' && item[item.Length - 1] == '/')
                    urlArray[i] = url + urlArray[i];

            }
            HashSet<string> temp = new HashSet<string>();
            char[] tempChar = url.ToCharArray();
            string siteEtalon = "";
            for (int i = 0; i < tempChar.Length - 1; i++)
            {
                string st = tempChar[i].ToString() + tempChar[i + 1].ToString();
                if (st == "//")
                {
                    i += 2;
                    do
                    {
                        siteEtalon += tempChar[i];
                        i += 1;
                    } while (i < tempChar.Length && tempChar[i] != '/');
                }
            }
            if (siteEtalon.Contains("www"))
                siteEtalon = siteEtalon.Remove(0, 4);
            foreach (string site in urlArray)
            {
                if (site.Contains(siteEtalon))
                {
                    string str = site;
                    if (!str.Contains("http"))
                    {
                        if ((str[0] == '/') & (str[1] == '/'))
                            str = "http:" + str;
                        else str = "http:/" + str;
                    }
                    char ch = str[str.Length - 1];

                    if (ch == '/')
                        str = str.Remove(str.Length - 1, 1);
                    temp.Add(str);
                }
            };
            List<string> answerList = new List<string>(temp);
            return answerList;
        }
    }
}