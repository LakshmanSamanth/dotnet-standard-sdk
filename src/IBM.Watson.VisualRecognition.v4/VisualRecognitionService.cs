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
using IBM.Cloud.SDK.Core.Model;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.VisualRecognition.v4.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.VisualRecognition.v4
{
    public partial class VisualRecognitionService : IBMService, IVisualRecognitionService
    {
        const string defaultServiceName = "visual_recognition";
        private const string defaultServiceUrl = "https://api.us-south.visual-recognition.watson.cloud.ibm.com";
        public string Version { get; set; }

        public VisualRecognitionService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public VisualRecognitionService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public VisualRecognitionService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public VisualRecognitionService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public VisualRecognitionService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
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
        /// Analyze images.
        ///
        /// Analyze images by URL, by file, or both against your own collection. Make sure that
        /// **training_status.objects.ready** is `true` for the feature before you use a collection to analyze images.
        ///
        /// Encode the image and .zip file names in UTF-8 if they contain non-ASCII characters. The service assumes
        /// UTF-8 encoding if it encounters non-ASCII characters.
        /// </summary>
        /// <param name="collectionIds">The IDs of the collections to analyze.</param>
        /// <param name="features">The features to analyze.</param>
        /// <param name="imagesFile">An array of image files (.jpg or .png) or .zip files with images.
        /// - Include a maximum of 20 images in a request.
        /// - Limit the .zip file to 100 MB.
        /// - Limit each image file to 10 MB.
        ///
        /// You can also include an image with the **image_url** parameter. (optional)</param>
        /// <param name="imageUrl">An array of URLs of image files (.jpg or .png).
        /// - Include a maximum of 20 images in a request.
        /// - Limit each image file to 10 MB.
        /// - Minimum width and height is 30 pixels, but the service tends to perform better with images that are at
        /// least 300 x 300 pixels. Maximum is 5400 pixels for either height or width.
        ///
        /// You can also include images with the **images_file** parameter. (optional)</param>
        /// <param name="threshold">The minimum score a feature must have to be returned. (optional)</param>
        /// <returns><see cref="AnalyzeResponse" />AnalyzeResponse</returns>
        public DetailedResponse<AnalyzeResponse> Analyze(List<string> collectionIds, List<string> features, List<FileWithMetadata> imagesFile = null, List<string> imageUrl = null, float? threshold = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (collectionIds == null || collectionIds.Count == 0)
            {
                throw new ArgumentNullException("`collectionIds` is required for `Analyze`");
            }
            if (features == null || features.Count == 0)
            {
                throw new ArgumentNullException("`features` is required for `Analyze`");
            }
            DetailedResponse<AnalyzeResponse> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (collectionIds != null)
                {
                    foreach (string item in collectionIds)
                    {
                        var collectionIdsContent = new StringContent(item, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                        collectionIdsContent.Headers.ContentType = null;
                        formData.Add(collectionIdsContent, "collection_ids");
                    }
                }

                if (features != null)
                {
                    foreach (string item in features)
                    {
                        var featuresContent = new StringContent(item, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                        featuresContent.Headers.ContentType = null;
                        formData.Add(featuresContent, "features");
                    }
                }

                if (imagesFile != null)
                {
                    foreach (FileWithMetadata item in imagesFile)
                    {
                        var imagesFileContent = new ByteArrayContent(item.Data.ToArray());
                        System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                        System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(item.ContentType, out contentType);
                        imagesFileContent.Headers.ContentType = contentType;
                        formData.Add(imagesFileContent, "images_file", item.Filename);
                    }
                }

                if (imageUrl != null)
                {
                    foreach (string item in imageUrl)
                    {
                        var imageUrlContent = new StringContent(item, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                        imageUrlContent.Headers.ContentType = null;
                        formData.Add(imageUrlContent, "image_url");
                    }
                }

                if (threshold != null)
                {
                    var thresholdContent = new StringContent(threshold.ToString(), Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    thresholdContent.Headers.ContentType = null;
                    formData.Add(thresholdContent, "threshold");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/analyze");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "Analyze"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AnalyzeResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AnalyzeResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for Analyze.
        /// </summary>
        public class AnalyzeEnums
        {
            /// <summary>
            /// The features to analyze.
            /// </summary>
            public class FeaturesValue
            {
                /// <summary>
                /// Constant OBJECTS for objects
                /// </summary>
                public const string OBJECTS = "objects";
                
            }
        }
        /// <summary>
        /// Create a collection.
        ///
        /// Create a collection that can be used to store images.
        ///
        /// To create a collection without specifying a name and description, include an empty JSON object in the
        /// request body.
        ///
        /// Encode the name and description in UTF-8 if they contain non-ASCII characters. The service assumes UTF-8
        /// encoding if it encounters non-ASCII characters.
        /// </summary>
        /// <param name="name">The name of the collection. The name can contain alphanumeric, underscore, hyphen, and
        /// dot characters. It cannot begin with the reserved prefix `sys-`. (optional)</param>
        /// <param name="description">The description of the collection. (optional)</param>
        /// <param name="trainingStatus">Training status information for the collection. (optional)</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> CreateCollection(string name = null, string description = null, TrainingStatus trainingStatus = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/collections");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (trainingStatus != null)
                {
                    bodyObject["training_status"] = JToken.FromObject(trainingStatus);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "CreateCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List collections.
        ///
        /// Retrieves a list of collections for the service instance.
        /// </summary>
        /// <returns><see cref="CollectionsList" />CollectionsList</returns>
        public DetailedResponse<CollectionsList> ListCollections()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<CollectionsList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "ListCollections"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CollectionsList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CollectionsList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get collection details.
        ///
        /// Get details of one collection.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> GetCollection(string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "GetCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a collection.
        ///
        /// Update the name or description of a collection.
        ///
        /// Encode the name and description in UTF-8 if they contain non-ASCII characters. The service assumes UTF-8
        /// encoding if it encounters non-ASCII characters.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="name">The name of the collection. The name can contain alphanumeric, underscore, hyphen, and
        /// dot characters. It cannot begin with the reserved prefix `sys-`. (optional)</param>
        /// <param name="description">The description of the collection. (optional)</param>
        /// <param name="trainingStatus">Training status information for the collection. (optional)</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> UpdateCollection(string collectionId, string name = null, string description = null, TrainingStatus trainingStatus = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `UpdateCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (trainingStatus != null)
                {
                    bodyObject["training_status"] = JToken.FromObject(trainingStatus);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "UpdateCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a collection.
        ///
        /// Delete a collection from the service instance.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCollection(string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v4/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "DeleteCollection"));
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
        /// Get a model.
        ///
        /// Download a model that you can deploy to detect objects in images. The collection must include a generated
        /// model, which is indicated in the response for the collection details as `"rscnn_ready": true`. If the value
        /// is `false`, train or retrain the collection to generate the model.
        ///
        /// Currently, the model format is specific to Android apps. For more information about how to deploy the model
        /// to your app, see the [Watson Visual Recognition on Android](https://github.com/matt-ny/rscnn) project in
        /// GitHub.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="feature">The feature for the model.</param>
        /// <param name="modelFormat">The format of the returned model.</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public DetailedResponse<System.IO.MemoryStream> GetModelFile(string collectionId, string feature, string modelFormat)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetModelFile`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(feature))
            {
                throw new ArgumentNullException("`feature` is required for `GetModelFile`");
            }
            if (string.IsNullOrEmpty(modelFormat))
            {
                throw new ArgumentNullException("`modelFormat` is required for `GetModelFile`");
            }
            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}/model");

                restRequest.WithHeader("Accept", "application/octet-stream");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(feature))
                {
                    restRequest.WithArgument("feature", feature);
                }
                if (!string.IsNullOrEmpty(modelFormat))
                {
                    restRequest.WithArgument("model_format", modelFormat);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "GetModelFile"));
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
        /// Enum values for GetModelFile.
        /// </summary>
        public class GetModelFileEnums
        {
            /// <summary>
            /// The feature for the model.
            /// </summary>
            public class FeatureValue
            {
                /// <summary>
                /// Constant OBJECTS for objects
                /// </summary>
                public const string OBJECTS = "objects";
                
            }
            /// <summary>
            /// The format of the returned model.
            /// </summary>
            public class ModelFormatValue
            {
                /// <summary>
                /// Constant RSCNN for rscnn
                /// </summary>
                public const string RSCNN = "rscnn";
                
            }
        }
        /// <summary>
        /// Add images.
        ///
        /// Add images to a collection by URL, by file, or both.
        ///
        /// Encode the image and .zip file names in UTF-8 if they contain non-ASCII characters. The service assumes
        /// UTF-8 encoding if it encounters non-ASCII characters.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="imagesFile">An array of image files (.jpg or .png) or .zip files with images.
        /// - Include a maximum of 20 images in a request.
        /// - Limit the .zip file to 100 MB.
        /// - Limit each image file to 10 MB.
        ///
        /// You can also include an image with the **image_url** parameter. (optional)</param>
        /// <param name="imageUrl">The array of URLs of image files (.jpg or .png).
        /// - Include a maximum of 20 images in a request.
        /// - Limit each image file to 10 MB.
        /// - Minimum width and height is 30 pixels, but the service tends to perform better with images that are at
        /// least 300 x 300 pixels. Maximum is 5400 pixels for either height or width.
        ///
        /// You can also include images with the **images_file** parameter. (optional)</param>
        /// <param name="trainingData">Training data for a single image. Include training data only if you add one image
        /// with the request.
        ///
        /// The `object` property can contain alphanumeric, underscore, hyphen, space, and dot characters. It cannot
        /// begin with the reserved prefix `sys-` and must be no longer than 32 characters. (optional)</param>
        /// <returns><see cref="ImageDetailsList" />ImageDetailsList</returns>
        public DetailedResponse<ImageDetailsList> AddImages(string collectionId, List<FileWithMetadata> imagesFile = null, List<string> imageUrl = null, string trainingData = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `AddImages`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<ImageDetailsList> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (imagesFile != null)
                {
                    foreach (FileWithMetadata item in imagesFile)
                    {
                        var imagesFileContent = new ByteArrayContent(item.Data.ToArray());
                        System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                        System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(item.ContentType, out contentType);
                        imagesFileContent.Headers.ContentType = contentType;
                        formData.Add(imagesFileContent, "images_file", item.Filename);
                    }
                }

                if (imageUrl != null)
                {
                    foreach (string item in imageUrl)
                    {
                        var imageUrlContent = new StringContent(item, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                        imageUrlContent.Headers.ContentType = null;
                        formData.Add(imageUrlContent, "image_url");
                    }
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new StringContent(trainingData, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    trainingDataContent.Headers.ContentType = null;
                    formData.Add(trainingDataContent, "training_data");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/collections/{collectionId}/images");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "AddImages"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ImageDetailsList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ImageDetailsList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List images.
        ///
        /// Retrieves a list of images in a collection.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <returns><see cref="ImageSummaryList" />ImageSummaryList</returns>
        public DetailedResponse<ImageSummaryList> ListImages(string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListImages`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<ImageSummaryList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}/images");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "ListImages"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ImageSummaryList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ImageSummaryList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get image details.
        ///
        /// Get the details of an image in a collection.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="imageId">The identifier of the image.</param>
        /// <returns><see cref="ImageDetails" />ImageDetails</returns>
        public DetailedResponse<ImageDetails> GetImageDetails(string collectionId, string imageId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetImageDetails`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(imageId))
            {
                throw new ArgumentNullException("`imageId` is required for `GetImageDetails`");
            }
            else
            {
                imageId = Uri.EscapeDataString(imageId);
            }
            DetailedResponse<ImageDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}/images/{imageId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "GetImageDetails"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ImageDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ImageDetails>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete an image.
        ///
        /// Delete one image from a collection.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="imageId">The identifier of the image.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteImage(string collectionId, string imageId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteImage`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(imageId))
            {
                throw new ArgumentNullException("`imageId` is required for `DeleteImage`");
            }
            else
            {
                imageId = Uri.EscapeDataString(imageId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v4/collections/{collectionId}/images/{imageId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "DeleteImage"));
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
        /// Get a JPEG file of an image.
        ///
        /// Download a JPEG representation of an image.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="imageId">The identifier of the image.</param>
        /// <param name="size">The image size. Specify `thumbnail` to return a version that maintains the original
        /// aspect ratio but is no larger than 200 pixels in the larger dimension. For example, an original 800 x 1000
        /// image is resized to 160 x 200 pixels. (optional, default to full)</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public DetailedResponse<System.IO.MemoryStream> GetJpegImage(string collectionId, string imageId, string size = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetJpegImage`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(imageId))
            {
                throw new ArgumentNullException("`imageId` is required for `GetJpegImage`");
            }
            else
            {
                imageId = Uri.EscapeDataString(imageId);
            }
            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}/images/{imageId}/jpeg");

                restRequest.WithHeader("Accept", "image/jpeg");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(size))
                {
                    restRequest.WithArgument("size", size);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "GetJpegImage"));
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
        /// Enum values for GetJpegImage.
        /// </summary>
        public class GetJpegImageEnums
        {
            /// <summary>
            /// The image size. Specify `thumbnail` to return a version that maintains the original aspect ratio but is
            /// no larger than 200 pixels in the larger dimension. For example, an original 800 x 1000 image is resized
            /// to 160 x 200 pixels.
            /// </summary>
            public class SizeValue
            {
                /// <summary>
                /// Constant FULL for full
                /// </summary>
                public const string FULL = "full";
                /// <summary>
                /// Constant THUMBNAIL for thumbnail
                /// </summary>
                public const string THUMBNAIL = "thumbnail";
                
            }
        }
        /// <summary>
        /// List object metadata.
        ///
        /// Retrieves a list of object names in a collection.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <returns><see cref="ObjectMetadataList" />ObjectMetadataList</returns>
        public DetailedResponse<ObjectMetadataList> ListObjectMetadata(string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListObjectMetadata`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<ObjectMetadataList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}/objects");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "ListObjectMetadata"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ObjectMetadataList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ObjectMetadataList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update an object name.
        ///
        /// Update the name of an object. A successful request updates the training data for all images that use the
        /// object.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="_object">The name of the object.</param>
        /// <param name="newObject">The updated name of the object. The name can contain alphanumeric, underscore,
        /// hyphen, space, and dot characters. It cannot begin with the reserved prefix `sys-`.</param>
        /// <returns><see cref="UpdateObjectMetadata" />UpdateObjectMetadata</returns>
        public DetailedResponse<UpdateObjectMetadata> UpdateObjectMetadata(string collectionId, string _object, string newObject)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `UpdateObjectMetadata`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(_object))
            {
                throw new ArgumentNullException("`_object` is required for `UpdateObjectMetadata`");
            }
            else
            {
                _object = Uri.EscapeDataString(_object);
            }
            if (string.IsNullOrEmpty(newObject))
            {
                throw new ArgumentNullException("`newObject` is required for `UpdateObjectMetadata`");
            }
            DetailedResponse<UpdateObjectMetadata> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/collections/{collectionId}/objects/{_object}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newObject))
                {
                    bodyObject["object"] = newObject;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "UpdateObjectMetadata"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<UpdateObjectMetadata>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<UpdateObjectMetadata>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get object metadata.
        ///
        /// Get the number of bounding boxes for a single object in a collection.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="_object">The name of the object.</param>
        /// <returns><see cref="ObjectMetadata" />ObjectMetadata</returns>
        public DetailedResponse<ObjectMetadata> GetObjectMetadata(string collectionId, string _object)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetObjectMetadata`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(_object))
            {
                throw new ArgumentNullException("`_object` is required for `GetObjectMetadata`");
            }
            else
            {
                _object = Uri.EscapeDataString(_object);
            }
            DetailedResponse<ObjectMetadata> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/collections/{collectionId}/objects/{_object}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "GetObjectMetadata"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ObjectMetadata>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ObjectMetadata>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete an object.
        ///
        /// Delete one object from a collection. A successful request deletes the training data from all images that use
        /// the object.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="_object">The name of the object.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteObject(string collectionId, string _object)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteObject`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(_object))
            {
                throw new ArgumentNullException("`_object` is required for `DeleteObject`");
            }
            else
            {
                _object = Uri.EscapeDataString(_object);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v4/collections/{collectionId}/objects/{_object}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "DeleteObject"));
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
        /// Train a collection.
        ///
        /// Start training on images in a collection. The collection must have enough training data and untrained data
        /// (the **training_status.objects.data_changed** is `true`). If training is in progress, the request queues the
        /// next training job.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> Train(string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `Train`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/collections/{collectionId}/train");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "Train"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add training data to an image.
        ///
        /// Add, update, or delete training data for an image. Encode the object name in UTF-8 if it contains non-ASCII
        /// characters. The service assumes UTF-8 encoding if it encounters non-ASCII characters.
        ///
        /// Elements in the request replace the existing elements.
        ///
        /// - To update the training data, provide both the unchanged and the new or changed values.
        ///
        /// - To delete the training data, provide an empty value for the training data.
        /// </summary>
        /// <param name="collectionId">The identifier of the collection.</param>
        /// <param name="imageId">The identifier of the image.</param>
        /// <param name="objects">Training data for specific objects. (optional)</param>
        /// <returns><see cref="TrainingDataObjects" />TrainingDataObjects</returns>
        public DetailedResponse<TrainingDataObjects> AddImageTrainingData(string collectionId, string imageId, List<TrainingDataObject> objects = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `AddImageTrainingData`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(imageId))
            {
                throw new ArgumentNullException("`imageId` is required for `AddImageTrainingData`");
            }
            else
            {
                imageId = Uri.EscapeDataString(imageId);
            }
            DetailedResponse<TrainingDataObjects> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v4/collections/{collectionId}/images/{imageId}/training_data");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (objects != null && objects.Count > 0)
                {
                    bodyObject["objects"] = JToken.FromObject(objects);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "AddImageTrainingData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingDataObjects>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingDataObjects>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get training usage.
        ///
        /// Information about the completed training events. You can use this information to determine how close you are
        /// to the training limits for the month.
        /// </summary>
        /// <param name="startTime">The earliest day to include training events. Specify dates in YYYY-MM-DD format. If
        /// empty or not specified, the earliest training event is included. (optional)</param>
        /// <param name="endTime">The most recent day to include training events. Specify dates in YYYY-MM-DD format.
        /// All events for the day are included. If empty or not specified, the current day is used. Specify the same
        /// value as `start_time` to request events for a single day. (optional)</param>
        /// <returns><see cref="TrainingEvents" />TrainingEvents</returns>
        public DetailedResponse<TrainingEvents> GetTrainingUsage(string startTime = null, string endTime = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<TrainingEvents> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v4/training_usage");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(startTime))
                {
                    restRequest.WithArgument("start_time", startTime);
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    restRequest.WithArgument("end_time", endTime);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "GetTrainingUsage"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingEvents>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingEvents>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](https://cloud.ibm.com/docs/visual-recognition?topic=visual-recognition-information-security).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteUserData(string customerId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException("`customerId` is required for `DeleteUserData`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v4/user_data");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(customerId))
                {
                    restRequest.WithArgument("customer_id", customerId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("watson_vision_combined", "v4", "DeleteUserData"));
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
    }
}
