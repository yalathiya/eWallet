$("#loginForm").submit(function (e) {
  e.preventDefault();
  var email = $("#email")[0].value;
  var password = $("#password")[0].value;
  //   console.log(email);
  //   console.log(password);

  // Define the data you want to send
  var credential = {
    G01f01: email,
    G01f02: password,
  };

  //   console.log(credential);

  $.ajax({
    type: "POST",
    url: "https://localhost:7278/api/CLAuth",
    data: JSON.stringify(credential), // Serialize the data to JSON
    contentType: "application/json", // Set content type to JSON
    success: function (response) {
      if (response.HasError) {
        alert("Invalid Credential");
      } else {
        var token = response.data.token;
        //   console.log(token);
        sessionStorage.setItem("token", token);

        $.ajax({
          type: "GET",
          url: "https://localhost:7278/api/CLUser/info",
          headers: {
            Authorization: "Bearer " + token,
          },
          success: function (response) {
            console.log("usrData", response);
            sessionStorage.setItem("firstName", response.data.firstName);
            sessionStorage.setItem("lastName", response.data.lastName);
            sessionStorage.setItem("emailId", response.data.emailId);
            sessionStorage.setItem("mobileNumber", response.data.mobileNumber);
          },
          error: function (xhr, status, error) {
            console.log(xhr + " " + status + " " + error);
            alert(error);
          },
        });
      }
    },
    error: function (xhr, status, error) {
      console.error("Error:", error);
      alert(error);
    },
  });
});
