﻿namespace Teference.Zoho.Api
{
    #region Namespace

    using System.IO;
    using System.Threading.Tasks;

    #endregion

    public interface IZsInvoiceApi
    {
        Task<ZsInvoice> GetAsync(string id);
        Task<ZsInvoice> GetAsync(string apiBaseUrl, string authToken, string organizationId, string id);

        Task<ZsInvoices> GetAllAsync(ZsPage page = null);
        Task<ZsInvoices> GetAllAsync(string apiBaseUrl, string authToken, string organizationId, ZsPage page = null);

        Task<ZsInvoices> GetAllAsync(ZsInvoiceFilter filterType, string filterId, ZsPage page = null);
        Task<ZsInvoices> GetAllAsync(string apiBaseUrl, string authToken, string organizationId, ZsInvoiceFilter filterType, string filterId, ZsPage page = null);

        Task<ZsInvoice> CollectCharge(string id, string cardId);
        Task<ZsInvoice> CollectCharge(string apiBaseUrl, string authToken, string organizationId, string id, string cardId);

        Task<bool> ConvertToVoid(string id);
        Task<bool> ConvertToVoid(string apiBaseUrl, string authToken, string organizationId, string id);

        Task<bool> ConvertToOpen(string id);
        Task<bool> ConvertToOpen(string apiBaseUrl, string authToken, string organizationId, string id);

        Task<bool> EmailInvoice(string id, ZsInvoiceEmailInput emailInput);
        Task<bool> EmailInvoice(string apiBaseUrl, string authToken, string organizationId, string id, ZsInvoiceEmailInput emailInput);

        Task<bool> WriteOff(string id);
        Task<bool> WriteOff(string apiBaseUrl, string authToken, string organizationId, string id);

        Task<bool> CancelWriteOff(string id);
        Task<bool> CancelWriteOff(string apiBaseUrl, string authToken, string organizationId, string id);

        Task<Stream> GetPdfAsync(string id);
        Task<Stream> GetPdfAsync(string apiBaseUrl, string authToken, string organizationId, string id);
    }
}