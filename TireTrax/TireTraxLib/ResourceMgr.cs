using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace TireTraxLib
{
    using ResourceManager = System.Resources.ResourceManager;


    public static class ResourceMgr
    {
        private static readonly ResourceManager controlsResourceManager =
            new ResourceManager(typeof(ResourceMgr).Namespace + ".Resources.Controls.Controls",
                                                 Assembly.GetAssembly(typeof(ResourceMgr)));

        private static readonly ResourceManager messagesResourceManager =
            new ResourceManager(typeof(ResourceMgr).Namespace + ".Resources.Messages.Messages",
                                                 Assembly.GetAssembly(typeof(ResourceMgr)));

        private static readonly ResourceManager errorsResourceManager =
            new ResourceManager(typeof(ResourceMgr).Namespace + ".Resources.Errors.Errors",
                                                 Assembly.GetAssembly(typeof(ResourceMgr)));



        #region Generic Manger


        public static ResourceManager GetResourceManager(string resourceFilePath)
        {
            return new ResourceManager(
                string.Format("{0}Resources.{1}", typeof(ResourceMgr).Name, resourceFilePath),
                Assembly.GetAssembly(typeof(ResourceMgr))
                );
        }


        public static string GetResourceText(ResourceManager resourceManager, string key)
        {
            return GetResourceText(resourceManager, key, CultureInfo.CurrentCulture);
        }



        public static string GetResourceText(ResourceManager resourceManager, string key, params string[] percentSignReplaceableParameters)
        {
            return GetPercentReplacedParametersString(
                resourceManager.GetString(key, CultureInfo.CurrentUICulture),
                percentSignReplaceableParameters);
        }



        public static string GetResourceText(ResourceManager resourceManager, string key, CultureInfo cultureInfo)
        {
            string s = resourceManager.GetString(key, cultureInfo);
            return string.IsNullOrEmpty(s) ? key : s;
        }


        public static string GetResourceText(ResourceManager resourceManager, string key, CultureInfo cultureInfo, params string[] percentSignReplaceableParameters)
        {
            string s = resourceManager.GetString(key, cultureInfo);
            return GetPercentReplacedParametersString(s, percentSignReplaceableParameters);
        }


        #endregion


        #region Controls


        /// <summary>
        /// Gets the control text for the current culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetControlText(string key)
        {
            return GetControlText(key, CultureInfo.CurrentCulture);
        }



        /// <summary>
        /// Gets the control text for the current culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="percentSignReplaceableParameters">The percent sign replaceable parameters.</param>
        /// <returns>
        /// If the key is found then the resource string replaced with the paramters other wise the key is returned
        /// </returns>
        /// <remarks>
        /// Example
        /// =======
        /// Message
        ///     resource message = "Hello %0 from %1"
        /// Function Call:
        ///     GetControlText ("message", "user", "Powerbar");
        /// Return value:
        ///     Hello user from Powerbar
        /// </remarks>
        public static string GetControlText(string key, params string[] percentSignReplaceableParameters)
        {
            return
                GetPercentReplacedParametersString(
                    controlsResourceManager.GetString(key, CultureInfo.CurrentUICulture),
                    percentSignReplaceableParameters);
        }



        /// <summary>
        /// Gets the control text for the supplied culture
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public static string GetControlText(string key, CultureInfo cultureInfo)
        {
            string s = controlsResourceManager.GetString(key, cultureInfo);
            return string.IsNullOrEmpty(s) ? key : s;
        }



        /// <summary>
        /// Gets the control text for the specified culture with replaceable parameters.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <param name="percentSignReplaceableParameters">The percent sign replaceable parameters.</param>
        /// <returns></returns>
        public static string GetControlText(string key, CultureInfo cultureInfo,
                                            params string[] percentSignReplaceableParameters)
        {
            string s = controlsResourceManager.GetString(key, cultureInfo);
            return GetPercentReplacedParametersString(s, percentSignReplaceableParameters);
        }


        #endregion


        #region Messages


        /// <summary>
        /// Gets the message for the current culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetMessage(string key)
        {
            return GetMessage(key, CultureInfo.CurrentCulture);
        }



        /// <summary>
        /// Gets the message for the current culture with replaceable parameters.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="percentSignReplaceableParameters">The percent sign replaceable parameters.</param>
        /// <returns></returns>
        public static string GetMessage(string key, params string[] percentSignReplaceableParameters)
        {
            return GetPercentReplacedParametersString(
                messagesResourceManager.GetString(key, CultureInfo.CurrentCulture), percentSignReplaceableParameters);
        }



        /// <summary>
        /// Gets the message for the specified cultrue.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public static string GetMessage(string key, CultureInfo cultureInfo)
        {
            string s = messagesResourceManager.GetString(key, cultureInfo);
            return string.IsNullOrEmpty(s) ? key : s;
        }



        /// <summary>
        /// Gets the message for the specified culture with replaceable parameters.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <param name="percentSignReplaceableParameters">The percent sign replaceable parameters.</param>
        /// <returns></returns>
        public static string GetMessage(string key, CultureInfo cultureInfo,
                                        params string[] percentSignReplaceableParameters)
        {
            string message = GetMessage(key, cultureInfo);
            return GetPercentReplacedParametersString(message, percentSignReplaceableParameters);
        }


        #endregion


        #region Errors


        /// <summary>
        /// Gets the error message for the current culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetError(string key)
        {
            return GetError(key, CultureInfo.CurrentCulture);
        }



        /// <summary>
        /// Gets the error message for the sepcified culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public static string GetError(string key, CultureInfo cultureInfo)
        {
            string s = errorsResourceManager.GetString(key, cultureInfo);
            return string.IsNullOrEmpty(s) ? key : s;
        }



        /// <summary>
        /// Gets the error message with replaceable parameters.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="percentSignReplacebaleParameters">The percent sign replacebale parameters.</param>
        /// <returns></returns>
        public static string GetError(string key, params string[] percentSignReplacebaleParameters)
        {
            return GetPercentReplacedParametersString(errorsResourceManager.GetString(key, CultureInfo.CurrentCulture));
        }



        /// <summary>
        /// Gets the error message for the specified culture with the 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <param name="percentReplaceableParamters">The percent replaceable paramters.</param>
        /// <returns></returns>
        public static string GetError(string key, CultureInfo cultureInfo, params string[] percentReplaceableParamters)
        {
            return GetPercentReplacedParametersString(GetError(key, cultureInfo));
        }


        #endregion


        /// <summary>
        /// Gets the percent replaced parameters string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="percentReplaceableParamters">The percent replaceable paramters.</param>
        /// <returns></returns>
        public static string GetPercentReplacedParametersString(string text, params string[] percentReplaceableParamters)
        {
            if (!string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < percentReplaceableParamters.Length; i++)
                {
                    if (text.Contains("%" + i))
                    {
                        text = text.Replace("%" + i, percentReplaceableParamters[i]);
                    }
                }

                return text;
            }

            return text;
        }


        /// THIS METHOD IS JUST FOR MAINTAINENCE PURPOSES
        /// <summary>
        /// Cleans the resources.
        /// </summary>
        public static void CleanResources()
        {
            ResourceSet resourceSet = controlsResourceManager.GetResourceSet(new CultureInfo("en-US"), true, true);
            IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();

            NameValueCollection list = new NameValueCollection();
            ArrayList keysList = new ArrayList();


            while (enumerator.MoveNext())
            {
                string key = (string)enumerator.Key;
                key = key.Replace(' ', '_').ToLower();
                list.Add(key, (string)enumerator.Value);
                keysList.Add(key);
            }

            string[] array = new string[keysList.Count];
            for (int i = 0; i < keysList.Count; i++)
            {
                array[i] = (string)keysList[i];
            }

            Array.Sort(array);

            StreamWriter writer = File.CreateText("c:\\controls.txt");

            foreach (string s in array)
            {
                writer.WriteLine(s + "=" + list[s]);
            }

            writer.Close();

        }
    }
}
