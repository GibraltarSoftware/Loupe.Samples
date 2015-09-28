using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Loupe.Extension.FogBugz.Internal
{
    /// <summary>
    /// Provides access to a FogBugz server
    /// </summary>
    internal class FBApi
    {
        public FBApi(string url, string email, string password)
        {
            Url = url;
            Login(email, password);
        }

        public void Login(string email, string password)
        {
            Email = email;
            Password = password;
            try
            {
                m_fromLogin = true;
                Dictionary<string, string> args = new Dictionary<string, string>
                                                      {
                                                          {"email", Email},
                                                          {"password", Password}
                                                      };
                XmlDocument doc = XCmd("logon", args);
                XmlNodeList tokens = doc.GetElementsByTagName("token");
                if (tokens.Count != 1) throw new Exception(String.Format("Unexpected result from FogBugz:\r\n{0}", doc.InnerXml));
                else Token = tokens[0].InnerText;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to log into FogBugz with the provided Username and Password.", ex);
            }
            finally
            {
                m_fromLogin = false;
            }
        }


        public void Logout()
        {
            Token = string.Empty;
        }

        public string Cmd(string cmd)
        {
            return Cmd(cmd, new Dictionary<string, string>());
        }

        public XmlDocument XCmd(string cmd)
        {
            return XCmd(cmd, new Dictionary<string, string>());
        }

        public XmlNodeList XCmd(string cmd, string xPath)
        {
            return XCmd(cmd, new Dictionary<string, string>(), xPath);
        }

        public string Cmd(string cmd, Dictionary<string, string> args)
        {
            return Cmd(cmd, args, null);
        }

        private readonly Dictionary<string, byte[]>[] m_emptyFiles = new Dictionary<string, byte[]>[] { };
        //private Dictionary<string, string> m_emptyArgs = new Dictionary<string, string>();

        public XmlDocument XCmd(string cmd, Dictionary<string, string> args)
        {

            return XCmd(cmd, args, m_emptyFiles);
        }

        public XmlNodeList XCmd(string cmd, Dictionary<string, string> args, string xPath)
        {
            return XCmd(cmd, args, null, xPath);
        }


        public XmlDocument XCmd(string cmd, Dictionary<string, string> args, Dictionary<string, byte[]>[] files)
        {
            return DocFromXml(Cmd(cmd, args, files));
        }

        private static XmlDocument DocFromXml(string result)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            return doc;
        }

        public XmlNodeList XCmd(string cmd, Dictionary<string, string> args, Dictionary<string, byte[]>[] files, string xPath)
        {
            return XCmd(cmd, args, files).SelectNodes(xPath);
        }


        public string Cmd(string cmd, Dictionary<string, string> args, Dictionary<string, byte[]>[] files)
        {
            //  Create the url
            if (args == null) args = new Dictionary<string, string>();
            args.Add("cmd", cmd);
            if (!String.IsNullOrEmpty(Token)) args.Add("token", Token);
            return CallRESTAPIFiles(Url, args, files);
        }

        public delegate void ApiEvent(object sender, string sent, string received);
        public event ApiEvent ApiCalled;

        //
        // CallRestAPIFiles submits an API request to the FogBugz api using the 
        // multipart/form-data submission method (so you can add files)
        // Don't forget to include nFileCount in your rgArgs collection if you are adding files.
        //
        private string CallRESTAPIFiles(string sURL, Dictionary<string, string> rgArgs, Dictionary<string, byte[]>[] rgFiles)
        {

            string sBoundaryString = getRandomString(30);
            string sBoundary = "--" + sBoundaryString;
            ASCIIEncoding encoding = new ASCIIEncoding();
            UTF8Encoding utf8encoding = new UTF8Encoding();
            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(sURL);
            http.Method = "POST";
            http.AllowWriteStreamBuffering = true;
            http.ContentType = "multipart/form-data; boundary=" + sBoundaryString;
            const string vbCrLf = "\r\n";

            Queue parts = new Queue();

            //
            // add all the normal arguments
            //
            foreach (System.Collections.Generic.KeyValuePair<string, string> i in rgArgs)
            {
                parts.Enqueue(encoding.GetBytes(sBoundary + vbCrLf));
                parts.Enqueue(encoding.GetBytes("Content-Type: text/plain; charset=\"utf-8\"" + vbCrLf));
                parts.Enqueue(encoding.GetBytes("Content-Disposition: form-data; name=\"" + i.Key + "\"" + vbCrLf + vbCrLf));
                parts.Enqueue(utf8encoding.GetBytes(i.Value));
                parts.Enqueue(encoding.GetBytes(vbCrLf));
            }

            //
            // add all the files
            //
            if (rgFiles != null)
            {
                foreach (Dictionary<string, byte[]> j in rgFiles)
                {
                    parts.Enqueue(encoding.GetBytes(sBoundary + vbCrLf));
                    parts.Enqueue(encoding.GetBytes("Content-Disposition: form-data; name=\""));
                    parts.Enqueue(j["name"]);
                    parts.Enqueue(encoding.GetBytes("\"; filename=\""));
                    parts.Enqueue(j["filename"]);
                    parts.Enqueue(encoding.GetBytes("\"" + vbCrLf));
                    parts.Enqueue(encoding.GetBytes("Content-Transfer-Encoding: base64" + vbCrLf));
                    parts.Enqueue(encoding.GetBytes("Content-Type: "));
                    parts.Enqueue(j["contenttype"]);
                    parts.Enqueue(encoding.GetBytes(vbCrLf + vbCrLf));
                    parts.Enqueue(j["data"]);
                    parts.Enqueue(encoding.GetBytes(vbCrLf));
                }
            }

            parts.Enqueue(encoding.GetBytes(sBoundary + "--"));

            //
            // calculate the content length
            //
            int nContentLength = 0;
            foreach (Byte[] part in parts)
            {
                nContentLength += part.Length;
            }
            http.ContentLength = nContentLength;

            //
            // write the post
            //
            Stream stream = http.GetRequestStream();
            string sent = "";
            foreach (Byte[] part in parts)
            {
                stream.Write(part, 0, part.Length);
                sent += encoding.GetString(part);
            }
            stream.Close();

            //
            // read the result
            //
            Stream r = http.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(r);
            string retValue = reader.ReadToEnd();
            reader.Close();
            if (ApiCalled != null) ApiCalled(this, sent, retValue);
            return retValue;
        }

        private static string getRandomString(int nLength)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
            string s = "";
            System.Random rand = new System.Random();
            for (int i = 0; i < nLength; i++)
            {
                int rnum = (int)Math.Floor((double)rand.Next(0, chars.Length - 1));
                s += chars.Substring(rnum, 1);
            }
            return s;
        }

        /// <summary>
        /// The Url to connect the API to
        /// </summary>
        public string Url { get; private set; }

        private bool m_fromLogin;
        private string m_token;
        private string Token
        {
            get
            {
                if (!m_fromLogin && String.IsNullOrEmpty(m_token)) throw new Exception("Not logged in...");
                return m_token;
            }
            set { m_token = value; }
        }

        private string Email { get; set; }

        private string Password { get; set; }

        #region Strongly Typed API
        // These are strongly typed methods that wrap specific commands (mostly taken from the APITesting projects
        // provided on the FogBugz blog.

        #region source data

        #region Search

        public string Search(string q)
        {
            return Search(q, "ixBug,sEvent,sTitle,ixProject");
        }

        public XmlNodeList XSearch(string q)
        {
            return XSearch(q, "ixBug,sEvent,sTitle,ixProject");
        }

        public string Search(string q, string cols)
        {
            return Search(q, cols, 0);
        }

        public XmlNodeList XSearch(string q, string cols)
        {
            return XSearch(q, cols, 0);
        }

        public string Search(string q, string cols, int max)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"q", String.Format("{0}", q)},
                               {"cols", String.Format("{0}", cols)},
                               {"max", String.Format("{0}", (max <= 0 ? 10000 : max))}
                           };
            return Cmd("search", args);
        }

        public XmlNodeList XSearch(string q, string cols, int max)
        {
            return FBApi.DocFromXml(Search(q, cols, max)).SelectNodes("/response/cases/case");
        }
 

        #endregion

        #region Editing CaseIds
        public string ListAreas()
        {
            return ListAreas(false);
        }

        public XmlNodeList XListAreas()
        {
            return XListAreas(false);
        }

        public string ListAreas(bool onlyWritable)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (onlyWritable)
                args.Add("fWrite", "1");
            return Cmd("listAreas", args);
        }

        public XmlNodeList XListAreas(bool onlyWritable)
        {
            return DocFromXml(ListAreas(onlyWritable)).SelectNodes("/response/areas/area");
        }

        public XmlNodeList XListPriorities()
        {
            return DocFromXml(Cmd("listPriorities")).SelectNodes("/response/priorities/priority");
        }

        public string ListProjects()
        {
            return ListProjects(false);
        }

        public XmlNodeList XListProjects()
        {
            return XListProjects(false);
        }

        public string ListProjects(bool onlyWritable)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (onlyWritable)
                args.Add("fWrite", "1");
            return Cmd("listProjects", args);
        }

        public XmlNodeList XListProjects(bool onlyWritable)
        {
            return FBApi.DocFromXml(ListProjects(onlyWritable)).SelectNodes("/response/projects/project");
        }


        public string ListCategories()
        {
            return Cmd("listCategories");
        }

        public XmlNodeList XListCategories()
        {
            return FBApi.DocFromXml(ListCategories()).SelectNodes("/response/categories/category");
        }

        public string ListFixFors(int ixProject)
        {
            Dictionary<string, string> args = new Dictionary<string, string> {{"ixProject", ixProject.ToString()}};
            return Cmd("listFixFors", args);
        }

        public XmlNodeList XListFixFors(int ixProject)
        {
            return FBApi.DocFromXml(ListFixFors(ixProject)).SelectNodes("/response/fixfors/fixfor");
        }

        /// <summary>
        /// Get a dictionary of all of the projects and their areas
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<String>> ListProjectsAndAreas()
        {
            Dictionary<string, List<string>> returnVal = new Dictionary<string, List<string>>();

            //we can get a list of all of the AREAS which includes the project names, and assemble both lists in one call.
            XmlNodeList areas = XListAreas(true);

            foreach (XmlNode areaNode in areas)
            {
                XmlNode areaNameNode = areaNode.SelectSingleNode("./sArea");
                XmlNode projectNameNode = areaNode.SelectSingleNode("./sProject");
                string area = areaNameNode.InnerText;
                string project = projectNameNode.InnerText;

                //we are using a composite dictionary & list to store this info:
                List<string> areaList;
                if (returnVal.TryGetValue(project, out areaList) == false)
                {
                    areaList = new List<string>();
                    returnVal.Add(project, areaList);
                }

                //I'm not absolutely sure if FB allows a duplicate area name, but we can't handle it, so be sure...
                if (areaList.Contains(area) == false)
                {
                    areaList.Add(area);
                }
            }

            return returnVal;
        }

        /// <summary>
        /// Get the set of all priorities with their labels
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> ListPriorities()
        {
            Dictionary<int, string> returnVal = new Dictionary<int, string>();

            XmlNodeList priorities = XListPriorities();

            foreach (XmlNode priorityNode in priorities)
            {
                XmlNode priorityIdNode = priorityNode.SelectSingleNode("./ixPriority");
                XmlNode priorityNameNode = priorityNode.SelectSingleNode("./sPriority");
                int id = Convert.ToInt32(priorityIdNode.InnerText);
                string name = priorityNameNode.InnerText;
                
                returnVal.Add(id, name);
            }

            return returnVal;
        }

        #endregion

        #region Scheduling
        public string StartWork(int ixBug)
        {
            Dictionary<string, string> args = new Dictionary<string, string> {{"ixBug", ixBug.ToString()}};
            return Cmd("startWork", args);
        }

        public XmlDocument XStartWork(int ixBug)
        {
            return FBApi.DocFromXml(StartWork(ixBug));
        }

        public string StopWork()
        {
            return Cmd("stopWork");
        }

        public XmlDocument XStopWork()
        {
            return FBApi.DocFromXml(StopWork());
        }

        public string NewInterval(int ixBug, DateTime dtStart, DateTime dtEnd)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"ixBug", ixBug.ToString()},
                               {"dtStart", dtStart.ToUniversalTime().ToString("s") + "Z"},
                               {"dtEnd", dtEnd.ToUniversalTime().ToString("s") + "Z"}
                           };
            return Cmd("newInterval", args);
        }

        public XmlDocument XNewInterval(int ixBug, DateTime dtStart, DateTime dtEnd)
        {
            return FBApi.DocFromXml(NewInterval(ixBug, dtStart, dtEnd));
        }

        public string ListIntervals(DateTime dtStart, DateTime dtEnd)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"dtStart", dtStart.ToUniversalTime().ToString("s") + "Z"},
                               {"dtEnd", dtEnd.ToUniversalTime().ToString("s") + "Z"}
                           };
            return Cmd("listIntervals", args);
        }

        public XmlNodeList XListIntervals(DateTime dtStart, DateTime dtEnd)
        {
            return FBApi.DocFromXml(ListIntervals(dtStart, dtEnd)).GetElementsByTagName("interval");
        }
        public XmlNodeList XListIntervals(DateTime dtStart, DateTime dtEnd, int ixBug)
        {
            return FBApi.DocFromXml(ListIntervals(dtStart, dtEnd)).SelectNodes(String.Format("/response/intervals/interval[ixBug='{0}']", ixBug));
        }
        
        #endregion

        #region Discussion Group
        /*
        private void btnLoadGroups_Click(object sender, EventArgs e)
        {
            FillDropDownWithResults("listDiscussGroups", listGroups, null, "response/discussions/discussion", "sFullName", "ixDiscussGroup");
        }

        private void cmdListDiscussion_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            if (txtixdiscussgroup.Text.Length > 0)
                args.Add("ixDiscussGroup", txtixdiscussgroup.Text);
            else
                args.Add("ixDiscussGroup", getIX(listGroups.Text).ToString());

            if (fFull.Checked)
                args.Add("fFull", "1");

            args.Add("m", txtm.Text);
            args.Add("y", txty.Text);

            Api.Cmd("listDiscussion", args);
        }

        private void cmdListDiscussTopic_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            args.Add("ixDiscussTopic", txtixdiscusstopic.Text);

            Api.Cmd("listDiscussion", args);
        }
        */
        #endregion

        #region Subscriptions
        public string Subscribe(int ixBug, int ixWikiPage)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"ixBug", ixBug.ToString()},
                               {"ixWikiPage", ixWikiPage.ToString()}
                           };
            return Cmd("subscribe", args);
        }
        public XmlDocument XSubscribe(int ixBug, int ixWikiPage)
        {
            return FBApi.DocFromXml(Subscribe(ixBug, ixWikiPage));
        }

        public string Unsubscribe(int ixBug, int ixWikiPage)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"ixBug", ixBug.ToString()},
                               {"ixWikiPage", ixWikiPage.ToString()}
                           };
            return Cmd("unsubscribe", args);
        }

        public XmlDocument XUnsubscribe(int ixBug, int ixWikiPage)
        {
            return FBApi.DocFromXml(Unsubscribe(ixBug, ixWikiPage));
        }

        public string View(int ixBug)
        {
            Dictionary<string, string> args = new Dictionary<string, string> {{"ixBug", ixBug.ToString()}};
            return Cmd("view", args);
        }

        public XmlDocument XView(int ixBug)
        {
            return FBApi.DocFromXml(View(ixBug));
        }

        #endregion

        #endregion

        #endregion

    }
}