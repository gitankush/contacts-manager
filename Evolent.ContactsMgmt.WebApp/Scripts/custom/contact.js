
$(document).ready(function () {
    getContacts();
});
//Load All Contacts
function getContacts() {
    $.ajax({
        url: "/Contacts/GetAllContacts",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var html = '';
            if (data != null && data.length>0) {
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td style="display:none;"><input type="hidden" value=\'' + item.ContactID + '\'</td>';
                    html += '<td>' + item.FirstName + '</td>';
                    html += '<td>' + item.LastName + '</td>';
                    html += '<td>' + item.Email + '</td>';
                    html += '<td>' + item.PhoneNumber + '</td>';
                    html += '<td>' + (item.Status === true ? "Active" : "Inactive") + '</td>';
                    html += '<td><button class="btn btn-primary btn-xs" title="Edit" onclick="return getbyID(' + item.ContactID + ')"><span class="glyphicon glyphicon-pencil"></span></button>&nbsp;<button class="btn btn-danger btn-xs" title="Delete" onclick="Delete(' + item.ContactID + ')"><span class="glyphicon glyphicon-trash"></span></a></td>';
                    html += '</tr>';
                });
            }
            else {
                html = "<td colspan='7'>No Contacts Found!!!</td>";
            }
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Contact Function   
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var contact = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        Status: $("#Status").prop("checked")
    };
    $.ajax({
        url: "/Contacts/AddContact",
        data: JSON.stringify(contact),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (result.success) {
                alert(result.message);
                getContacts();
                $('#myModal').modal('hide');
            }
            else {
                alert(result.message);
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Get Contact by ID
function getbyID(ContactID) {
    $('#FirstName').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#PhoneNumber').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Contacts/GetContact?contactID=" + ContactID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ContactID').val(result.ContactID);
            $('#FirstName').val(result.FirstName);
            $('#LastName').val(result.LastName);
            $('#Email').val(result.Email);
            $('#PhoneNumber').val(result.PhoneNumber);
            if (result.Status) {
                $('#Status').prop('checked',true);
            }
            else {
                $('#Status').prop('checked',false);

            }
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//Update Contact Function   
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var contact = {
        ContactID: $('#ContactID').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        Status: $("#Status").prop("checked")
    };
    $.ajax({
        url: "/Contacts/UpdateContact",
        data: JSON.stringify(contact),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                alert(result.message);
                getContacts();
                $('#myModal').modal('hide');
            }
            else {
                alert(result.message);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Delete Contact Function
function Delete(ContactID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Contacts/DeleteContact?contactID=" + ContactID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                alert(result.message);
                getContacts();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ContactID').val("");
    $('#FirstName').val("");
    $('#LastName').val("");
    $('#Email').val("");
    $('#PhoneNumber').val("");
    $('#Status').prop('checked', true);
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('.textInput').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#FirstName').val().trim() === "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }
    if ($('#LastName').val().trim() === "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() === "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#PhoneNumber').val().trim() === "") {
        $('#PhoneNumber').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PhoneNumber').css('border-color', 'lightgrey');
    }
    return isValid;
}  