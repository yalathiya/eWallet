using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Utilities;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Net;
using iText.Layout;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implementation of IBLWalletHandler
    /// </summary>
    public class BLWlt01Handler : IBLWlt01Handler
    {
        #region Private Members

        /// <summary>
        /// OrmLite dbFactory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuring dependency injection
        /// </summary>
        /// <param name="dbFactory"> OrmLite database factory </param>
        /// <param name="notificationService"> notification service </param>
        public BLWlt01Handler(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;  
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get current balance of wallet
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claims</param>
        /// <returns> object of response </returns>
        public Response GetCurrentBalance(int walletId)
        {
            _objResponse = new Response();

            using(var db = _dbFactory.Open())
            {
                double currentBalance = db.SingleById<Wlt01>(walletId).T01f03;

                var data = new { currentBalance};

                _objResponse.SetResponse("Fetched Current Balance", data);
                return _objResponse;
            }
        }

        /// <summary>
        /// Validate DTO model
        /// </summary>
        /// <param name="objDTOIvl"> object of interval </param>
        /// <returns> object of response </returns>
        public Response Validate(DTOIvl01 objDTOIvl)
        {
            _objResponse = new Response();

            if(objDTOIvl.L01f01 > DateTime.Now || objDTOIvl.L01f02 > DateTime.Now)
            {
                _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "interval time exceeds current time", null);
                return _objResponse;
            }

            if(objDTOIvl.L01f01 > objDTOIvl.L01f02)
            {
                _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "bad interval", null);
                return _objResponse;
            }

            return _objResponse;

        }

        /// <summary>
        /// Generate file bytes for statements
        /// </summary>
        /// <param name="objDTOIvl"> object of interval </param>
        /// <returns> byte array </returns>
        public byte[] GenerateFileBytes(DTOIvl01 objDTOIvl)
        {
            // Create a memory stream to hold the PDF content
            MemoryStream stream = new MemoryStream();

            // Create a new PDF document
            using (PdfWriter writer = new PdfWriter(stream))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Add a new page to the PDF
                    Document document = new Document(pdf);
                    document.Add(new Paragraph("Hello, World!"));

                    // Close the document
                    document.Close();
                }
            }

            // Set the position of the memory stream to the beginning
            stream.Position = 0;

            return stream.ToArray();
        }

        #endregion
    }
}
