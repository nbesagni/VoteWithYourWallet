// My JavaScript Document
// Login Modal Tab Selection
$("#signInButtonMain").click(function () {
    activateTab('signIn');
});

$("#signUpButtonMain").click(function () {
    activateTab('signUp');
    $("#reg_log_modal_header_text").css();
});

function activateTab(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
}

// Registration form validation
// Check passwords match
function checkPasswordMatch() {
    var password = $("#register-password1").val();
    var confirmPassword = $("#register-password2").val();

    if (password !== confirmPassword) {
        $("#register-password2").removeClass('is-valid');
        $("#register-password2").addClass('is-invalid');
        $('#passwordNoMatch').show();
    }
    else {
        $("#register-password2").removeClass('is-invalid');
        $("#register-password2").addClass('is-valid');
        $('#passwordMatch').show();
    }
}
// Check password length
function checkPasswordLength() {
    var password = $("#register-password1").val();

    if ((password.length > 5) && (password.length < 21)) {
        $("#register-password1").removeClass('is-invalid');
        $("#register-password1").addClass('is-valid');
    }
    else {
        $("#register-password1").removeClass('is-valid');
        $("#register-password1").addClass('is-invalid');
    }
}
// Call Registration validation functions
$(document).ready(function () {
    $('#passwordNoMatch').hide();
    $('#passwordMatch').hide();
    $("#register-password1, #register-password2").keyup(checkPasswordMatch);
    $("#register-password1, #register-password2").keyup(checkPasswordLength);
});
// Disable registration button until all fields entered and validations met
$(document).ready(function () {
    $("#registerButton").attr("disabled", "disabled");
    $("#register-password1, #register-password2, #register-firstName, #register-lastName, #register-email").keyup(function () {
        var validated = false;
        var password = $("#register-password1").val();
        var confirmPassword = $("#register-password2").val();
        var firstName = $("#register-firstName").val();
        var lastName = $("#register-lastName").val();
        var email = $("#register-email").val();
        if (password.length > 5 && password.length < 21 && confirmPassword.length !== 0 && firstName.length !== 0 && lastName.length !== 0 && email.length !== 0 && password === confirmPassword) {
            validated = true;
        }
        if (validated) {
            $("#registerButton").removeAttr("disabled");
        }
    });
});
// Disable log in button until all fields entered and validations met
$(document).ready(function () {
    $("#loginButton").attr("disabled", "disabled");
    $("#signIn-password, #signIn-email").keyup(function () {
        var validated = false;
        var password = $("#signIn-password").val();
        var email = $("#signIn-email").val();
        if (password.length !== 0 && email.length !== 0) {
            validated = true;
        }
        if (validated) {
            $("#loginButton").removeAttr("disabled");
        }
    });
});

// Disable log in button until all fields entered and validations met
$(document).ready(function () {
    $("#causeCreateButton").attr("disabled", "disabled");
    $("#causeName, #causeDescription").keyup(function () {
        var validated = false;
        var causeName = $("#causeName").val();
        var causeDescription = $("#causeDescription").val();
        if (causeName.length !== 0 && causeDescription.length !== 0) {
            validated = true;
        }
        if (validated) {
            $("#causeCreateButton").removeAttr("disabled");
        }
    });
});

// include comma seperators in signature counts
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

$(document).ready(function () {
    var signatureCount = parseInt($('.signatureCount').text());
    signatureCount = numberWithCommas(signatureCount);
    $('.signatureCount').text(signatureCount);
});

// Update signature count and add signature when cause is signed
//$("#signatureButton").click(function () {
//    var signatureCount2 = parseInt($('.signatureCount').text().replace(',', ''));
//    signatureCount2++;
//    signatureCount2 = numberWithCommas(signatureCount2);
//    $('.signatureCount').text(signatureCount2);
//    $("#signaturesList tbody").prepend($('<tr class="newSignatureRow"><th scope="row"><span class="oi oi-person"></span> You</th><td><small>Just now</small></td></tr>'));
//    $("#signatureButton").attr("disabled", "disabled");
//});


