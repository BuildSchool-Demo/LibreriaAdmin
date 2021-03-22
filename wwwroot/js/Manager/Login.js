let jwtAuthUrl = "https://localhost:5001/api/Manager/Login";

function ajax() {
    $.ajax({
        url: jwtAuthUrl,
        method: "POST",
        dataType: "json",
        data: loginVM,
        success: function (response) {
            localStorage.setItem("jwtToken", response.token);

            $("#token").html("JWT Token: " + response.token);
        }
    });
}
