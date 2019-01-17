﻿namespace Teference.Zoho.Api
{
    #region Namespace

    using Internals;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    internal sealed class ZsSubscriptionApi : IZsSubscriptionApi
    {
        #region Variable declaration

        private IZohoClient client;

        #endregion

        #region Constructor

        public ZsSubscriptionApi(IZohoClient client)
        {
            this.client = client;
        }

        #endregion

        #region Methods

        public async Task<ZsSubscription> GetAsync(string id)
        {
            this.client.Configuration.CheckConfig();
            return await this.GetAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id);
        }
        public async Task<ZsSubscription> GetAsync(string apiBaseUrl, string authToken, string organizationId, string id)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.GetAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsGetSubscription, id));
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        public async Task<ZsSubscriptions> GetAllAsync(ZsPage page = null)
        {
            this.client.Configuration.CheckConfig();
            return await this.GetAllAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, page);
        }
        public async Task<ZsSubscriptions> GetAllAsync(string apiBaseUrl, string authToken, string organizationId, ZsPage page = null)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.GetAsync(page.AppendTo(ApiResources.ZsGetSubscriptionsAll));
                var processResult = await response.ProcessResponse<ZsSubscriptions>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data;
            }
        }

        public async Task<ZsSubscriptions> GetAllAsync(ZsSubscriptionFilter filterType, string filterId, ZsPage page = null)
        {
            this.client.Configuration.CheckConfig();
            return await this.GetAllAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, filterType, filterId, page);
        }
        public async Task<ZsSubscriptions> GetAllAsync(string apiBaseUrl, string authToken, string organizationId, ZsSubscriptionFilter filterType, string filterId, ZsPage page = null)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);

                var requestUri = new QueryStringBuilder(ApiResources.ZsGetSubscriptionsAll);
                switch (filterType)
                {
                    case ZsSubscriptionFilter.CustomerId:
                        requestUri.Add("customer_id", filterId);
                        break;
                }

                var response = await httpClient.GetAsync(page.AppendTo(requestUri).ToString());
                var processResult = await response.ProcessResponse<ZsSubscriptions>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data;
            }
        }

        public async Task<ZsSubscription> CreateAsync(ZsSubscriptionCreate createInput)
        {
            this.client.Configuration.CheckConfig();
            return await this.CreateAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, createInput);
        }
        public async Task<ZsSubscription> CreateAsync(string apiBaseUrl, string authToken, string organizationId, ZsSubscriptionCreate createInput)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (createInput == null)
            {
                throw new ArgumentNullException("createInput");
            }

            //var validationResult = createInput.Validate();
            //if (!string.IsNullOrWhiteSpace(validationResult))
            //{
            //    throw new ArgumentException(validationResult);
            //}

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var content = new StringContent(
                    JsonConvert.SerializeObject(
                        createInput,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    Encoding.UTF8,
                    "application/json");
                var response = await httpClient.PostAsync(ApiResources.ZsPostSubscription, content);
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        public async Task<ZsSubscription> UpdateAsync(string id, ZsSubscriptionUpdate updateInput)
        {
            this.client.Configuration.CheckConfig();
            return await this.UpdateAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, updateInput);
        }
        public async Task<ZsSubscription> UpdateAsync(string apiBaseUrl, string authToken, string organizationId, string id, ZsSubscriptionUpdate updateInput)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (updateInput == null)
            {
                throw new ArgumentNullException("updateInput");
            }

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            //var validationResult = updateInput.Validate();
            //if (!string.IsNullOrWhiteSpace(validationResult))
            //{
            //    throw new ArgumentException(validationResult);
            //}

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        updateInput,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPutSubscription, id), content);
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        public async Task<ZsSubscription> AutoCollectAsync(string id, bool isAutoCollect)
        {
            this.client.Configuration.CheckConfig();
            return await this.AutoCollectAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, isAutoCollect);
        }
        public async Task<ZsSubscription> AutoCollectAsync(string apiBaseUrl, string authToken, string organizationId, string id, bool isAutoCollect)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            var subscriptionAutoCollectJson = new ZsSubscriptionAutoCollectJson { IsAutoCollect = isAutoCollect };
            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        subscriptionAutoCollectJson,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionAutoCollect, id), content);
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        public async Task<bool> AssociateCouponAsync(string id, string couponCode)
        {
            this.client.Configuration.CheckConfig();
            return await this.AssociateCouponAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, couponCode);
        }
        public async Task<bool> AssociateCouponAsync(string apiBaseUrl, string authToken, string organizationId, string id, string couponCode)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionAssociateCoupon, id, couponCode), null);
                var processResult = await response.ProcessResponse<bool>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data;
            }
        }

        public async Task<bool> RemoveCouponAsync(string id)
        {
            this.client.Configuration.CheckConfig();
            return await this.RemoveCouponAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id);
        }
        public async Task<bool> RemoveCouponAsync(string apiBaseUrl, string authToken, string organizationId, string id)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.DeleteAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsDeleteSubscriptionRemoveCoupon, id));
                var processResult = await response.ProcessResponse<bool>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data;
            }
        }

        public async Task<ZsInvoice> AddChargeAsync(string id, double amount, string description)
        {
            this.client.Configuration.CheckConfig();
            return await this.AddChargeAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, amount, description);
        }
        public async Task<ZsInvoice> AddChargeAsync(string apiBaseUrl, string authToken, string organizationId, string id, double amount, string description)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (amount <= 0)
            {
                throw new ArgumentNullException("amount");
            }

            if (string.IsNullOrEmpty(description) || description == string.Empty)
            {
                throw new ArgumentNullException("description");
            }

            var subscriptionAddChargeJson = new ZsSubscriptionAddChargeJson { Amount = amount, Description = description };
            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        subscriptionAddChargeJson,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionAddCharge, id), content);
                var processResult = await response.ProcessResponse<ZsInvoiceJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Invoice;
            }
        }
        
        public async Task<ZsInvoice> BuyOnetimeAddon(string id, string addon_code, int quantity, double price, string tax_id, string addon_description)
        {
            this.client.Configuration.CheckConfig();
            return await this.BuyOnetimeAddon(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, addon_code, quantity, price, tax_id, addon_description);
        }
        public async Task<ZsInvoice> BuyOnetimeAddon(string apiBaseUrl, string authToken, string organizationId, string id, string addon_code, int quantity, double price, string tax_id, string addon_description)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (quantity <= 0)
            {
                throw new ArgumentNullException("quantity");
            }

            if (string.IsNullOrEmpty(addon_code) || addon_code == string.Empty)
            {
                throw new ArgumentNullException("addon_code");
            }

            var subscriptionAddAddonJson = new ZsSubscriptionBuyOnetimeAddonJson { AddonCode = addon_code, Quantity = quantity, Price = price, TaxId = tax_id, AddonDescription = addon_description};
            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        new { addons = new []{ subscriptionAddAddonJson } },
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionBuyOnetimeAddon, id), content);
                var processResult = await response.ProcessResponse<ZsInvoiceJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Invoice;
            }
        }

        public async Task<ZsSubscription> AddContactPerson(string id, List<string> contactPersons)
        {
            this.client.Configuration.CheckConfig();
            return await this.AddContactPerson(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, contactPersons);
        }
        public async Task<ZsSubscription> AddContactPerson(string apiBaseUrl, string authToken, string organizationId, string id, List<string> contactPersons)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (null == contactPersons || contactPersons.Count == 0)
            {
                throw new ArgumentNullException("contactPersons");
            }

            var contactPersonsList = new ZsSubscriptionAddContactsJson();
            contactPersonsList.ContactPersons = new List<ZsSubscriptionAddContactJson>();
            foreach (var item in contactPersons)
            {
                contactPersonsList.ContactPersons.Add(new ZsSubscriptionAddContactJson { ContactPersonId = item });
            }

            //var validationResult = updateInput.Validate();
            //if (!string.IsNullOrWhiteSpace(validationResult))
            //{
            //    throw new ArgumentException(validationResult);
            //}

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        contactPersonsList,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionAddContactPerson, id), content);
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        public async Task<ZsSubscriptionNote> PostponeRenewalAsync(string id, DateTime renewalAt)
        {
            this.client.Configuration.CheckConfig();
            return await this.PostponeRenewalAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, renewalAt);
        }
        public async Task<ZsSubscriptionNote> PostponeRenewalAsync(string apiBaseUrl, string authToken, string organizationId, string id, DateTime renewalAt)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (default(DateTime) == renewalAt)
            {
                throw new ArgumentNullException("renewalAt");
            }

            var subscriptionPostponeRenewalJson = new ZsSubscriptionPostponeRenewalJson { RenewalAt = renewalAt.Date.ToString("yyyy-MM-dd") };
            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        subscriptionPostponeRenewalJson,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionPostponeRenewal, id), content);
                var processResult = await response.ProcessResponse<ZsSubscriptionNoteJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Note;
            }
        }

        public async Task<ZsSubscriptionNote> AddNoteAsync(string id, string noteDescription)
        {
            this.client.Configuration.CheckConfig();
            return await this.AddNoteAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, noteDescription);
        }
        public async Task<ZsSubscriptionNote> AddNoteAsync(string apiBaseUrl, string authToken, string organizationId, string id, string noteDescription)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (string.IsNullOrEmpty(noteDescription) || noteDescription == string.Empty)
            {
                throw new ArgumentNullException("noteDescription");
            }

            var subscriptionAddNoteJson = new ZsSubscriptionAddNoteJson { Description = noteDescription };
            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var jsonContent = JsonConvert.SerializeObject(
                        subscriptionAddNoteJson,
                        Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionAddNote, id), content);
                var processResult = await response.ProcessResponse<ZsSubscriptionNoteJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Note;
            }
        }

        public async Task<bool> DeleteNoteAsync(string id, string noteId)
        {
            this.client.Configuration.CheckConfig();
            return await this.DeleteNoteAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, noteId);
        }
        public async Task<bool> DeleteNoteAsync(string apiBaseUrl, string authToken, string organizationId, string id, string noteId)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            if (string.IsNullOrEmpty(noteId) || noteId == string.Empty)
            {
                throw new ArgumentNullException("noteId");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.DeleteAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsDeleteSubscriptionDeleteNote, id, noteId));
                var processResult = await response.ProcessResponse<bool>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            this.client.Configuration.CheckConfig();
            return await this.DeleteAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id);
        }
        public async Task<bool> DeleteAsync(string apiBaseUrl, string authToken, string organizationId, string id)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Subscription Id is required");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.DeleteAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsDeleteSubscription, id));
                var processResult = await response.ProcessResponse<bool>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data;
            }
        }

        public async Task<ZsSubscription> CancelAsync(string id, bool cancelAtEndOfCurrentTerm)
        {
            this.client.Configuration.CheckConfig();
            return await this.CancelAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id, cancelAtEndOfCurrentTerm);
        }
        public async Task<ZsSubscription> CancelAsync(string apiBaseUrl, string authToken, string organizationId, string id, bool cancelAtEndOfCurrentTerm)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                // uri should be rewritting using QueryStringBuilder
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionCancel, id, cancelAtEndOfCurrentTerm.ToString().ToLowerInvariant()), null);
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        public async Task<ZsSubscription> ReactivateAsync(string id)
        {
            this.client.Configuration.CheckConfig();
            return await this.ReactivateAsync(this.client.Configuration.ApiBaseUrl, this.client.Configuration.AuthToken, this.client.Configuration.OrganizationId, id);
        }
        public async Task<ZsSubscription> ReactivateAsync(string apiBaseUrl, string authToken, string organizationId, string id)
        {
            apiBaseUrl.CheckConfigApiBaseUrl();
            authToken.CheckConfigAuthToken();
            organizationId.CheckConfigOrganizationId();

            if (string.IsNullOrEmpty(id) || id == string.Empty)
            {
                throw new ArgumentNullException("id");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.Configure(apiBaseUrl, organizationId, authToken);
                var response = await httpClient.PostAsync(string.Format(CultureInfo.InvariantCulture, ApiResources.ZsPostSubscriptionReactivate, id), null);
                var processResult = await response.ProcessResponse<ZsSubscriptionJson>();
                if (null != processResult.Error)
                {
                    throw processResult.Error;
                }

                return processResult.Data.Subscription;
            }
        }

        #endregion
    }
}