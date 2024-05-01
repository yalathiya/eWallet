using api_eWallet.Models.POCO;

namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// Interface of notification service 
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Send Notification 
        /// </summary>
        void SendNotification(Not01 objNot01);
    }
}
