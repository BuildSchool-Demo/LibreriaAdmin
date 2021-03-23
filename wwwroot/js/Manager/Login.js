let jwtAuthUrl = "https://localhost:5001/api/Manager/Login";

$("#submitLogin").click(function () {
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;

    loginVM = { "username": username, "password": password };


    $.ajax({
        url: jwtAuthUrl,
        method: "POST", 
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify(loginVM),
        headers: {
            //"content-type":"application/json; charset=utf-8"
        },
        success: function (response) {

            localStorage.setItem("jwtToken", response.token);

            $("#token").html("JWT Token: " + response.token);

        //    window.location = '/Home/Index';
        }
    });
});