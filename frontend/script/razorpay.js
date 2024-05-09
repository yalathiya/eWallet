$("#razorpayForm").submit(function (e) {
  e.preventDefault();
  var amount = $("#amount")[0].value * 100;
  //   console.log(amount);
  //   alert(amount);

  // Send the request to initiate the payment
  $.ajax({
    type: "POST",
    url: "https://localhost:7278/api/CLRazorpay/Initiate",
    data: { amount: amount },
    headers: {
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    },
    success: function (response) {
      // Handle success response
      // console.log("Payment initiated successfully:", response);
      var orderId = response.data;
      // Use the order ID and customer info to set options
      var options = {
        key: "rzp_test_8qr1ipXJ76cjtN",
        amount: amount,
        currency: "INR",
        name: "eWallet",
        description: "Deposit into eWallet",
        order_id: orderId,
        image:
          "https://img.freepik.com/premium-vector/e-wallet-logo-vector-art-inspiration_24599-185.jpg",
        prefill: {
          name:
            sessionStorage.getItem("firstName") +
            sessionStorage.getItem("lastName"),
          email: sessionStorage.getItem("emailId"),
          contact: sessionStorage.getItem("mobileNumber"),
        },
        notes: {
          address: "Hello World",
        },
        theme: {
          color: "#3399cc",
        },
        handler: function (response) {
          console.log("payment success", response);
          $.ajax({
            type: "POST",
            url: "https://localhost:7278/api/CLRazorpay/ProcessPayment",
            headers: {
              Authorization: "Bearer " + sessionStorage.getItem("token"),
            },
            data: JSON.stringify(response),
            contentType: "application/json",
            success: function (response) {
              if (!response.HasError) {
                alert("Payment Successful");
              } else {
                alert(response.Message);
              }
            },
            error: function (xhr, status, error) {
              alert(JSON.stringify(error));
            },
          });
        },
      };
      // Initialize Razorpay with options
      var rzp = new Razorpay(options);
      rzp.open();
      rzp.on("payment.failed", function (response) {
        alert(JSON.stringify(response));
      });
    },
    error: function (xhr, status, error) {
      // Handle error response
      console.error("Error initiating payment:", error);
    },
  });
});
