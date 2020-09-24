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
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.LanguageTranslator.v3.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.LanguageTranslator.v3
{
    public partial class LanguageTranslatorService : IBMService, ILanguageTranslatorService
    {
        const string defaultServiceName = "language_translator";
        private const string defaultServiceUrl = "https://api.us-south.language-translator.watson.cloud.ibm.com";
        public string Version { get; set; }

        public LanguageTranslatorService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public LanguageTranslatorService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public LanguageTranslatorService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public LanguageTranslatorService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public LanguageTranslatorService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
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
        /// List supported languages.
        ///
        /// Lists all supported languages. The method returns an array of supported languages with information about
        /// each language. Languages are listed in alphabetical order by language code (for example, `af`, `ar`).
        /// </summary>
        /// <returns><see cref="Languages" />Languages</returns>
        public DetailedResponse<Languages> ListLanguages()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Languages> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/languages");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListLanguages"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Languages>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Languages>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Translate.
        ///
        /// Translates the input text from the source language to the target language. Specify a model ID that indicates
        /// the source and target languages, or specify the source and target languages individually. You can omit the
        /// source language to have the service attempt to detect the language from the input text. If you omit the
        /// source language, the request must contain sufficient input text for the service to identify the source
        /// language.
        /// </summary>
        /// <param name="text">Input text in UTF-8 encoding. Multiple entries result in multiple translations in the
        /// response.</param>
        /// <param name="modelId">The model to use for translation. For example, `en-de` selects the IBM-provided base
        /// model for English-to-German translation. A model ID overrides the `source` and `target` parameters and is
        /// required if you use a custom model. If no model ID is specified, you must specify at least a target
        /// language. (optional)</param>
        /// <param name="source">Language code that specifies the language of the input text. If omitted, the service
        /// derives the source language from the input text. The input must contain sufficient text for the service to
        /// identify the language reliably. (optional)</param>
        /// <param name="target">Language code that specifies the target language for translation. Required if model ID
        /// is not specified. (optional)</param>
        /// <returns><see cref="TranslationResult" />TranslationResult</returns>
        public DetailedResponse<TranslationResult> Translate(List<string> text, string modelId = null, string source = null, string target = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (text == null)
            {
                throw new ArgumentNullException("`text` is required for `Translate`");
            }
            DetailedResponse<TranslationResult> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/translate");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (text != null && text.Count > 0)
                {
                    bodyObject["text"] = JToken.FromObject(text);
                }
                if (!string.IsNullOrEmpty(modelId))
                {
                    bodyObject["model_id"] = modelId;
                }
                if (!string.IsNullOrEmpty(source))
                {
                    bodyObject["source"] = source;
                }
                if (!string.IsNullOrEmpty(target))
                {
                    bodyObject["target"] = target;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "Translate"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationResult>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationResult>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List identifiable languages.
        ///
        /// Lists the languages that the service can identify. Returns the language code (for example, `en` for English
        /// or `es` for Spanish) and name of each language.
        /// </summary>
        /// <returns><see cref="IdentifiableLanguages" />IdentifiableLanguages</returns>
        public DetailedResponse<IdentifiableLanguages> ListIdentifiableLanguages()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<IdentifiableLanguages> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/identifiable_languages");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListIdentifiableLanguages"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<IdentifiableLanguages>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<IdentifiableLanguages>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Identify language.
        ///
        /// Identifies the language of the input text.
        /// </summary>
        /// <param name="text">Input text in UTF-8 format.</param>
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        public DetailedResponse<IdentifiedLanguages> Identify(string text)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `Identify`");
            }
            DetailedResponse<IdentifiedLanguages> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/identify");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "text/plain");
                var httpContent = new StringContent(JsonConvert.SerializeObject(text), Encoding.UTF8);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "Identify"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<IdentifiedLanguages>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<IdentifiedLanguages>();
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
        /// Lists available translation models.
        /// </summary>
        /// <param name="source">Specify a language code to filter results by source language. (optional)</param>
        /// <param name="target">Specify a language code to filter results by target language. (optional)</param>
        /// <param name="_default">If the `default` parameter isn't specified, the service returns all models (default
        /// and non-default) for each language pair. To return only default models, set this parameter to `true`. To
        /// return only non-default models, set this parameter to `false`. There is exactly one default model, the
        /// IBM-provided base model, per language pair. (optional)</param>
        /// <returns><see cref="TranslationModels" />TranslationModels</returns>
        public DetailedResponse<TranslationModels> ListModels(string source = null, string target = null, bool? _default = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<TranslationModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/models");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(source))
                {
                    restRequest.WithArgument("source", source);
                }
                if (!string.IsNullOrEmpty(target))
                {
                    restRequest.WithArgument("target", target);
                }
                if (_default != null)
                {
                    restRequest.WithArgument("default", _default);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create model.
        ///
        /// Uploads training files to customize a translation model. You can customize a model with a forced glossary or
        /// with a parallel corpus:
        /// * Use a *forced glossary* to force certain terms and phrases to be translated in a specific way. You can
        /// upload only a single forced glossary file for a model. The size of a forced glossary file for a custom model
        /// is limited to 10 MB.
        /// * Use a *parallel corpus* when you want your custom model to learn from general translation patterns in
        /// parallel sentences in your samples. What your model learns from a parallel corpus can improve translation
        /// results for input text that the model has not been trained on. You can upload multiple parallel corpora
        /// files with a request. To successfully train with parallel corpora, the corpora files must contain a
        /// cumulative total of at least 5000 parallel sentences. The cumulative size of all uploaded corpus files for a
        /// custom model is limited to 250 MB.
        ///
        /// Depending on the type of customization and the size of the uploaded files, training time can range from
        /// minutes for a glossary to several hours for a large parallel corpus. To create a model that is customized
        /// with a parallel corpus and a forced glossary, customize the model with a parallel corpus first and then
        /// customize the resulting model with a forced glossary.
        ///
        /// You can create a maximum of 10 custom models per language pair. For more information about customizing a
        /// translation model, including the formatting and character restrictions for data files, see [Customizing your
        /// model](https://cloud.ibm.com/docs/language-translator?topic=language-translator-customizing).
        ///
        /// #### Supported file formats
        ///
        ///  You can provide your training data for customization in the following document formats:
        /// * **TMX** (`.tmx`) - Translation Memory eXchange (TMX) is an XML specification for the exchange of
        /// translation memories.
        /// * **XLIFF** (`.xliff`) - XML Localization Interchange File Format (XLIFF) is an XML specification for the
        /// exchange of translation memories.
        /// * **CSV** (`.csv`) - Comma-separated values (CSV) file with two columns for aligned sentences and phrases.
        /// The first row contains the language code.
        /// * **TSV** (`.tsv` or `.tab`) - Tab-separated values (TSV) file with two columns for aligned sentences and
        /// phrases. The first row contains the language code.
        /// * **JSON** (`.json`) - Custom JSON format for specifying aligned sentences and phrases.
        /// * **Microsoft Excel** (`.xls` or `.xlsx`) - Excel file with the first two columns for aligned sentences and
        /// phrases. The first row contains the language code.
        ///
        /// You must encode all text data in UTF-8 format. For more information, see [Supported document formats for
        /// training
        /// data](https://cloud.ibm.com/docs/language-translator?topic=language-translator-customizing#supported-document-formats-for-training-data).
        ///
        ///
        /// #### Specifying file formats
        ///
        ///  You can indicate the format of a file by including the file extension with the file name. Use the file
        /// extensions shown in **Supported file formats**.
        ///
        /// Alternatively, you can omit the file extension and specify one of the following `content-type`
        /// specifications for the file:
        /// * **TMX** - `application/x-tmx+xml`
        /// * **XLIFF** - `application/xliff+xml`
        /// * **CSV** - `text/csv`
        /// * **TSV** - `text/tab-separated-values`
        /// * **JSON** - `application/json`
        /// * **Microsoft Excel** - `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
        ///
        /// For example, with `curl`, use the following `content-type` specification to indicate the format of a CSV
        /// file named **glossary**:
        ///
        /// `--form "forced_glossary=@glossary;type=text/csv"`.
        /// </summary>
        /// <param name="baseModelId">The ID of the translation model to use as the base for customization. To see
        /// available models and IDs, use the `List models` method. Most models that are provided with the service are
        /// customizable. In addition, all models that you create with parallel corpora customization can be further
        /// customized with a forced glossary.</param>
        /// <param name="forcedGlossary">A file with forced glossary terms for the source and target languages. The
        /// customizations in the file completely overwrite the domain translation data, including high frequency or
        /// high confidence phrase translations.
        ///
        /// You can upload only one glossary file for a custom model, and the glossary can have a maximum size of 10 MB.
        /// A forced glossary must contain single words or short phrases. For more information, see **Supported file
        /// formats** in the method description.
        ///
        /// *With `curl`, use `--form forced_glossary=@{filename}`.*. (optional)</param>
        /// <param name="parallelCorpus">A file with parallel sentences for the source and target languages. You can
        /// upload multiple parallel corpus files in one request by repeating the parameter. All uploaded parallel
        /// corpus files combined must contain at least 5000 parallel sentences to train successfully. You can provide a
        /// maximum of 500,000 parallel sentences across all corpora.
        ///
        /// A single entry in a corpus file can contain a maximum of 80 words. All corpora files for a custom model can
        /// have a cumulative maximum size of 250 MB. For more information, see **Supported file formats** in the method
        /// description.
        ///
        /// *With `curl`, use `--form parallel_corpus=@{filename}`.*. (optional)</param>
        /// <param name="name">An optional model name that you can use to identify the model. Valid characters are
        /// letters, numbers, dashes, underscores, spaces, and apostrophes. The maximum length of the name is 32
        /// characters. (optional)</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        public DetailedResponse<TranslationModel> CreateModel(string baseModelId, System.IO.MemoryStream forcedGlossary = null, System.IO.MemoryStream parallelCorpus = null, string name = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(baseModelId))
            {
                throw new ArgumentNullException("`baseModelId` is required for `CreateModel`");
            }
            DetailedResponse<TranslationModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (forcedGlossary != null)
                {
                    var forcedGlossaryContent = new ByteArrayContent(forcedGlossary.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    forcedGlossaryContent.Headers.ContentType = contentType;
                    formData.Add(forcedGlossaryContent, "forced_glossary", "filename");
                }

                if (parallelCorpus != null)
                {
                    var parallelCorpusContent = new ByteArrayContent(parallelCorpus.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    parallelCorpusContent.Headers.ContentType = contentType;
                    formData.Add(parallelCorpusContent, "parallel_corpus", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/models");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(baseModelId))
                {
                    restRequest.WithArgument("base_model_id", baseModelId);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    restRequest.WithArgument("name", name);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "CreateModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationModel>();
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
        /// Deletes a custom translation model.
        /// </summary>
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <returns><see cref="DeleteModelResult" />DeleteModelResult</returns>
        public DetailedResponse<DeleteModelResult> DeleteModel(string modelId)
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
            DetailedResponse<DeleteModelResult> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/models/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "DeleteModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteModelResult>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteModelResult>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get model details.
        ///
        /// Gets information about a translation model, including training status for custom models. Use this API call
        /// to poll the status of your customization request. A successfully completed training has a status of
        /// `available`.
        /// </summary>
        /// <param name="modelId">Model ID of the model to get.</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        public DetailedResponse<TranslationModel> GetModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<TranslationModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/models/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "GetModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List documents.
        ///
        /// Lists documents that have been submitted for translation.
        /// </summary>
        /// <returns><see cref="DocumentList" />DocumentList</returns>
        public DetailedResponse<DocumentList> ListDocuments()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<DocumentList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/documents");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListDocuments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Translate document.
        ///
        /// Submit a document for translation. You can submit the document contents in the `file` parameter, or you can
        /// reference a previously submitted document by document ID.
        /// </summary>
        /// <param name="file">The contents of the source file to translate.
        ///
        /// [Supported file
        /// types](https://cloud.ibm.com/docs/language-translator?topic=language-translator-document-translator-tutorial#supported-file-formats)
        ///
        /// Maximum file size: **20 MB**.</param>
        /// <param name="filename">The filename for file.</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="modelId">The model to use for translation. For example, `en-de` selects the IBM-provided base
        /// model for English-to-German translation. A model ID overrides the `source` and `target` parameters and is
        /// required if you use a custom model. If no model ID is specified, you must specify at least a target
        /// language. (optional)</param>
        /// <param name="source">Language code that specifies the language of the source document. If omitted, the
        /// service derives the source language from the input text. The input must contain sufficient text for the
        /// service to identify the language reliably. (optional)</param>
        /// <param name="target">Language code that specifies the target language for translation. Required if model ID
        /// is not specified. (optional)</param>
        /// <param name="documentId">To use a previously submitted document as the source for a new translation, enter
        /// the `document_id` of the document. (optional)</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        public DetailedResponse<DocumentStatus> TranslateDocument(System.IO.MemoryStream file, string filename, string fileContentType = null, string modelId = null, string source = null, string target = null, string documentId = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (file == null)
            {
                throw new ArgumentNullException("`file` is required for `TranslateDocument`");
            }
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("`filename` is required for `TranslateDocument`");
            }
            DetailedResponse<DocumentStatus> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", filename);
                }

                if (modelId != null)
                {
                    var modelIdContent = new StringContent(modelId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelIdContent.Headers.ContentType = null;
                    formData.Add(modelIdContent, "model_id");
                }

                if (source != null)
                {
                    var sourceContent = new StringContent(source, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    sourceContent.Headers.ContentType = null;
                    formData.Add(sourceContent, "source");
                }

                if (target != null)
                {
                    var targetContent = new StringContent(target, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    targetContent.Headers.ContentType = null;
                    formData.Add(targetContent, "target");
                }

                if (documentId != null)
                {
                    var documentIdContent = new StringContent(documentId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    documentIdContent.Headers.ContentType = null;
                    formData.Add(documentIdContent, "document_id");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/documents");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "TranslateDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for TranslateDocument.
        /// </summary>
        public class TranslateDocumentEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_POWERPOINT for application/powerpoint
                /// </summary>
                public const string APPLICATION_POWERPOINT = "application/powerpoint";
                /// <summary>
                /// Constant APPLICATION_MSPOWERPOINT for application/mspowerpoint
                /// </summary>
                public const string APPLICATION_MSPOWERPOINT = "application/mspowerpoint";
                /// <summary>
                /// Constant APPLICATION_X_RTF for application/x-rtf
                /// </summary>
                public const string APPLICATION_X_RTF = "application/x-rtf";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_XML for application/xml
                /// </summary>
                public const string APPLICATION_XML = "application/xml";
                /// <summary>
                /// Constant APPLICATION_VND_MS_EXCEL for application/vnd.ms-excel
                /// </summary>
                public const string APPLICATION_VND_MS_EXCEL = "application/vnd.ms-excel";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET for application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                /// <summary>
                /// Constant APPLICATION_VND_MS_POWERPOINT for application/vnd.ms-powerpoint
                /// </summary>
                public const string APPLICATION_VND_MS_POWERPOINT = "application/vnd.ms-powerpoint";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION for application/vnd.openxmlformats-officedocument.presentationml.presentation
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET for application/vnd.oasis.opendocument.spreadsheet
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET = "application/vnd.oasis.opendocument.spreadsheet";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION for application/vnd.oasis.opendocument.presentation
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION = "application/vnd.oasis.opendocument.presentation";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT for application/vnd.oasis.opendocument.text
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT = "application/vnd.oasis.opendocument.text";
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_RTF for application/rtf
                /// </summary>
                public const string APPLICATION_RTF = "application/rtf";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant TEXT_JSON for text/json
                /// </summary>
                public const string TEXT_JSON = "text/json";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                /// <summary>
                /// Constant TEXT_RICHTEXT for text/richtext
                /// </summary>
                public const string TEXT_RICHTEXT = "text/richtext";
                /// <summary>
                /// Constant TEXT_RTF for text/rtf
                /// </summary>
                public const string TEXT_RTF = "text/rtf";
                /// <summary>
                /// Constant TEXT_XML for text/xml
                /// </summary>
                public const string TEXT_XML = "text/xml";
                
            }
        }

        /// <summary>
        /// Get document status.
        ///
        /// Gets the translation status of a document.
        /// </summary>
        /// <param name="documentId">The document ID of the document.</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        public DetailedResponse<DocumentStatus> GetDocumentStatus(string documentId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `GetDocumentStatus`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<DocumentStatus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "GetDocumentStatus"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete document.
        ///
        /// Deletes a document.
        /// </summary>
        /// <param name="documentId">Document ID of the document to delete.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteDocument(string documentId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `DeleteDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/documents/{documentId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "DeleteDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get translated document.
        ///
        /// Gets the translated document associated with the given document ID.
        /// </summary>
        /// <param name="documentId">The document ID of the document that was submitted for translation.</param>
        /// <param name="accept">The type of the response: application/powerpoint, application/mspowerpoint,
        /// application/x-rtf, application/json, application/xml, application/vnd.ms-excel,
        /// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-powerpoint,
        /// application/vnd.openxmlformats-officedocument.presentationml.presentation, application/msword,
        /// application/vnd.openxmlformats-officedocument.wordprocessingml.document,
        /// application/vnd.oasis.opendocument.spreadsheet, application/vnd.oasis.opendocument.presentation,
        /// application/vnd.oasis.opendocument.text, application/pdf, application/rtf, text/html, text/json, text/plain,
        /// text/richtext, text/rtf, or text/xml. A character encoding can be specified by including a `charset`
        /// parameter. For example, 'text/html;charset=utf-8'. (optional)</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public DetailedResponse<System.IO.MemoryStream> GetTranslatedDocument(string documentId, string accept = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `GetTranslatedDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/documents/{documentId}/translated_document");


                if (!string.IsNullOrEmpty(accept))
                {
                    restRequest.WithHeader("Accept", accept);
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "GetTranslatedDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = new DetailedResponse<System.IO.MemoryStream>();
                result.Result = new System.IO.MemoryStream(restRequest.AsByteArray().Result);
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetTranslatedDocument.
        /// </summary>
        public class GetTranslatedDocumentEnums
        {
            /// <summary>
            /// The type of the response: application/powerpoint, application/mspowerpoint, application/x-rtf,
            /// application/json, application/xml, application/vnd.ms-excel,
            /// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-powerpoint,
            /// application/vnd.openxmlformats-officedocument.presentationml.presentation, application/msword,
            /// application/vnd.openxmlformats-officedocument.wordprocessingml.document,
            /// application/vnd.oasis.opendocument.spreadsheet, application/vnd.oasis.opendocument.presentation,
            /// application/vnd.oasis.opendocument.text, application/pdf, application/rtf, text/html, text/json,
            /// text/plain, text/richtext, text/rtf, or text/xml. A character encoding can be specified by including a
            /// `charset` parameter. For example, 'text/html;charset=utf-8'.
            /// </summary>
            public class AcceptValue
            {
                /// <summary>
                /// Constant APPLICATION_POWERPOINT for application/powerpoint
                /// </summary>
                public const string APPLICATION_POWERPOINT = "application/powerpoint";
                /// <summary>
                /// Constant APPLICATION_MSPOWERPOINT for application/mspowerpoint
                /// </summary>
                public const string APPLICATION_MSPOWERPOINT = "application/mspowerpoint";
                /// <summary>
                /// Constant APPLICATION_X_RTF for application/x-rtf
                /// </summary>
                public const string APPLICATION_X_RTF = "application/x-rtf";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_XML for application/xml
                /// </summary>
                public const string APPLICATION_XML = "application/xml";
                /// <summary>
                /// Constant APPLICATION_VND_MS_EXCEL for application/vnd.ms-excel
                /// </summary>
                public const string APPLICATION_VND_MS_EXCEL = "application/vnd.ms-excel";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET for application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                /// <summary>
                /// Constant APPLICATION_VND_MS_POWERPOINT for application/vnd.ms-powerpoint
                /// </summary>
                public const string APPLICATION_VND_MS_POWERPOINT = "application/vnd.ms-powerpoint";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION for application/vnd.openxmlformats-officedocument.presentationml.presentation
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET for application/vnd.oasis.opendocument.spreadsheet
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET = "application/vnd.oasis.opendocument.spreadsheet";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION for application/vnd.oasis.opendocument.presentation
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION = "application/vnd.oasis.opendocument.presentation";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT for application/vnd.oasis.opendocument.text
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT = "application/vnd.oasis.opendocument.text";
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_RTF for application/rtf
                /// </summary>
                public const string APPLICATION_RTF = "application/rtf";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant TEXT_JSON for text/json
                /// </summary>
                public const string TEXT_JSON = "text/json";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                /// <summary>
                /// Constant TEXT_RICHTEXT for text/richtext
                /// </summary>
                public const string TEXT_RICHTEXT = "text/richtext";
                /// <summary>
                /// Constant TEXT_RTF for text/rtf
                /// </summary>
                public const string TEXT_RTF = "text/rtf";
                /// <summary>
                /// Constant TEXT_XML for text/xml
                /// </summary>
                public const string TEXT_XML = "text/xml";
                
            }
        }
    }
}
