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
using api_eWallet.DL.Interfaces;
using System.Data;

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

        /// <summary>
        /// Db context of transaction
        /// </summary>
        private IDbTsn01Context _objDbTsn01Context;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuring dependency injection
        /// </summary>
        /// <param name="dbFactory"> OrmLite database factory </param>
        /// <param name="dbTsn01Context"> context of Tsn01 </param>
        public BLWlt01Handler(IDbConnectionFactory dbFactory,
                              IDbTsn01Context dbTsn01Context)
        {
            _dbFactory = dbFactory;  
            _objDbTsn01Context = dbTsn01Context;
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
        /// <param name="walletId"> wallet id </param>
        /// <returns> byte array </returns>
        public byte[] GenerateFileBytes(int walletId, DTOIvl01 objDTOIvl)
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

                    document.Add(new Paragraph("Statement"));

                    DataTable dtTransactions = (DataTable)_objDbTsn01Context.GetTransactions(walletId, objDTOIvl.L01f01, objDTOIvl.L01f02);

                    // Create a table with number of columns equal to the DataTable's column count
                    Table table = new Table(dtTransactions.Columns.Count);

                    // Add headers to the table
                    foreach (DataColumn column in dtTransactions.Columns)
                    {
                        table.AddHeaderCell(column.ColumnName);
                    }

                    // Add data rows to the table
                    foreach (DataRow row in dtTransactions.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            table.AddCell(item.ToString());
                        }
                    }

                    // Add the table to the document
                    document.Add(table);
                }
            }

            return stream.ToArray();
        }

        #endregion
    }
}
