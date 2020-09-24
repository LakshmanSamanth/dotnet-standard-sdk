/**
* (C) Copyright IBM Corp. 2020.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

/**
* IBM OpenAPI SDK Code Generator Version: 99-SNAPSHOT-7197cce3-20200922-152803
*/
 
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1
{
    public partial class NaturalLanguageUnderstandingService : IBMService, INaturalLanguageUnderstandingService
    {
        const string defaultServiceName = "natural_language_understanding";
        private const string defaultServiceUrl = "https://api.us-south.natural-language-understanding.watson.cloud.ibm.com";
        public string Version { get; set; }

        public NaturalLanguageUnderstandingService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public NaturalLanguageUnderstandingService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public NaturalLanguageUnderstandingService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public NaturalLanguageUnderstandingService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public NaturalLanguageUnderstandingService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException("`version` is required");
            }
            Version = version;

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// Analyze text.
        ///
        /// Analyzes text, HTML, or a public webpage for the following features:
        /// - Categories
        /// - Concepts
        /// - Emotion
        /// - Entities
        /// - Keywords
        /// - Metadata
        /// - Relations
        /// - Semantic roles
        /// - Sentiment
        /// - Syntax.
        ///
        /// If a language for the input text is not specified with the `language` parameter, the service [automatically
        /// detects the
        /// language](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-detectable-languages).
        /// </summary>
        /// <param name="features">Specific features to analyze the document for.</param>
        /// <param name="text">The plain text to analyze. One of the `text`, `html`, or `url` parameters is required.
        /// (optional)</param>
        /// <param name="html">The HTML file to analyze. One of the `text`, `html`, or `url` parameters is required.
        /// (optional)</param>
        /// <param name="url">The webpage to analyze. One of the `text`, `html`, or `url` parameters is required.
        /// (optional)</param>
        /// <param name="clean">Set this to `false` to disable webpage cleaning. For more information about webpage
        /// cleaning, see [Analyzing
        /// webpages](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-analyzing-webpages).
        /// (optional, default to true)</param>
        /// <param name="xpath">An [XPath
        /// query](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-analyzing-webpages#xpath)
        /// to perform on `html` or `url` input. Results of the query will be appended to the cleaned webpage text
        /// before it is analyzed. To analyze only the results of the XPath query, set the `clean` parameter to `false`.
        /// (optional)</param>
        /// <param name="fallbackToRaw">Whether to use raw HTML content if text cleaning fails. (optional, default to
        /// true)</param>
        /// <param name="returnAnalyzedText">Whether or not to return the analyzed text. (optional, default to
        /// false)</param>
        /// <param name="language">ISO 639-1 code that specifies the language of your text. This overrides automatic
        /// language detection. Language support differs depending on the features you include in your analysis. For
        /// more information, see [Language
        /// support](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-language-support).
        /// (optional)</param>
        /// <param name="limitTextCharacters">Sets the maximum number of characters that are processed by the service.
        /// (optional)</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        public DetailedResponse<AnalysisResults> Analyze(Features features, string text = null, string html = null, string url = null, bool? clean = null, string xpath = null, bool? fallbackToRaw = null, bool? returnAnalyzedText = null, string language = null, long? limitTextCharacters = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (features == null)
            {
                throw new ArgumentNullException("`features` is required for `Analyze`");
            }
            DetailedResponse<AnalysisResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/analyze");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (features != null)
                {
                    bodyObject["features"] = JToken.FromObject(features);
                }
                if (!string.IsNullOrEmpty(text))
                {
                    bodyObject["text"] = text;
                }
                if (!string.IsNullOrEmpty(html))
                {
                    bodyObject["html"] = html;
                }
                if (!string.IsNullOrEmpty(url))
                {
                    bodyObject["url"] = url;
                }
                if (clean != null)
                {
                    bodyObject["clean"] = JToken.FromObject(clean);
                }
                if (!string.IsNullOrEmpty(xpath))
                {
                    bodyObject["xpath"] = xpath;
                }
                if (fallbackToRaw != null)
                {
                    bodyObject["fallback_to_raw"] = JToken.FromObject(fallbackToRaw);
                }
                if (returnAnalyzedText != null)
                {
                    bodyObject["return_analyzed_text"] = JToken.FromObject(returnAnalyzedText);
                }
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                if (limitTextCharacters != null)
                {
                    bodyObject["limit_text_characters"] = JToken.FromObject(limitTextCharacters);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "Analyze"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AnalysisResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AnalysisResults>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List models.
        ///
        /// Lists Watson Knowledge Studio [custom entities and relations
        /// models](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-customizing)
        /// that are deployed to your Natural Language Understanding service.
        /// </summary>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        public DetailedResponse<ListModelsResults> ListModels()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ListModelsResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "ListModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListModelsResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListModelsResults>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete model.
        ///
        /// Deletes a custom model.
        /// </summary>
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <returns><see cref="DeleteModelResults" />DeleteModelResults</returns>
        public DetailedResponse<DeleteModelResults> DeleteModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DeleteModelResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "DeleteModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteModelResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteModelResults>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
