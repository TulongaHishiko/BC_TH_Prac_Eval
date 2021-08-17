// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Refresh Page Data
function refreshContactPage() {
    $tableBody = $("#tableInfo");
    $tableBody.empty();
    $.ajax({
        type: "GET",
        url: "/Contact/GetContacts",
        traditional: true,
        success: function (result) {
            if (result.length > 0) {
                $.each(result, function (i, item) {
                    var row = '<tr><td>' + item["name"] + '</td>' +
                        '<td>' + item["surname"] + '</td>' +
                        '<td>' + item["email"] + '</td>' +
                        '<td class="center-align">' + item["linkedClientsCount"] + '</td>' +
                        '<td><a class="tooltipped" href="Contact/Details?id=' + item["id"] +'"'+
                        'data-position="top" data-tooltip="Contact Details">' +
                        '<i class="material-icons">info</i></a>'+
                        '<a class="tooltipped" onclick="DeleteContact(' + item["id"] + ')"' +
                        'data-position="top" data-tooltip="Delete Contact">' +
                        '<i class="material-icons icon-red">delete</i></a></td></tr>';
                    $tableBody.append(row);
                });
            }

        }
    });
};
function refreshClientPage() {
    $tableBody = $("#tableInfo");
    $tableBody.empty();
    $.ajax({
        type: "GET",
        url: "/Client/GetClients",
        traditional: true,
        success: function (result) {
            if (result.length > 0) {
                $.each(result, function (i, item) {
                    var row = '<tr><td>' + item["name"] + '</td>' +
                        '<td>' + item["clientCode"] + '</td>' +
                        '<td class="center-align">' + item["linkedContactsCount"] + '</td>' +
                        '<td><a class="tooltipped" href="Client/Details?id=' + item["id"] + '"' +
                        'data-position="top" data-tooltip="Client Details">' +
                        '<i class="material-icons">info</i></a>' +
                        '<a class="tooltipped" onclick="DeleteClient(' + item["id"] + ')"' +
                        'data-position="top" data-tooltip="Delete Client">' +
                        '<i class="material-icons icon-red">delete</i></a></td></tr>';
                    $tableBody.append(row);
                    
                });
            } 
        }
    });
};
function refreshLinkedContacts(clientId) {
    $tableBody = $("#tableInfo");
    $tableBody.empty();
    $contacts = $("#contacts");
    $.ajax({
        type: "GET",
        url: "/Link/GetLinkedContacts?clientId=" + clientId,
        traditional: true,
        success: function (result) {
            if (result.length > 0) {
                $.each(result, function (i, item) {
                    var row = '<tr><td>' + item["surname"] + " " + item["name"] + '</td>' +
                        '<td>' + item["email"] + '</td>' +
                        '<td><a class="tooltipped" onclick="unlinkContact(' + clientId + ',' + item["id"] + ')"' +
                        'data-position="top" data-tooltip="Unlink Contact">' +
                        '<i class="material-icons">unlink</i></a></td></tr>';
                    $tableBody.append(row);
                });
            }
        }
    });
};
function refreshLinkedClients(contactId) {
    $tableBody = $("#tableInfo");
    $contacts = $("#contacts");
    $tableBody.empty();
    $.ajax({
        type: "GET",
        url: "/Link/GetLinkedClients?contactId=" + contactId,
        traditional: true,
        success: function (result) {
            if (result.length > 0) {
                $.each(result, function (i, item) {
                    var row = '<tr><td>' + item["name"] + '</td>' +
                        '<td>' + item["clientCode"] + '</td>' +
                        '<td><a class="tooltipped" onclick="unlinkClient(' + contactId + ',' + item["id"] + ')"' +
                        'data-position="top" data-tooltip="Unlink Client">' +
                        '<i class="material-icons">unlink</i></a></td></tr>';
                    $tableBody.append(row);
                });
            }

        }
    });
};

//load link selects
function loadLinkContactModal(clientId) {
    var link = "/Link/GetUnlinkedContacts?clientId=" + clientId;
    $.getJSON(link, function (result) {
        var options = $("#contactSelect");
        options.empty();
        
        $.each(result, function () {
            var fullName = this.name + " " + this.surname;
            options.append($("<option />").val(this.id).text(fullName));
        });
        $('select').formSelect();
    });
    $('#linkModal').modal('open');
}
function loadLinkClientModal(contactId) {
    var link = "/Link/GetUnlinkedClients?contactId=" + contactId;
    $.getJSON(link, function (result) {
        var options = $("#clientSelect");
        options.empty();
        //don't forget error handling!
        $.each(result, function () {
            options.append($("<option />").val(this.id).text(this.name));
        });
        $('select').formSelect();
    });
    $('#linkModal').modal('open');
}
//clear form fields
function clearForms() {
    $('#contactName').val('');
    $('#contactSurname').val('');
    $('#contactEmail').val('');
    $('#clientName').val('');
};

//Form submit buttons
$('#submitContactBtn').click(function () {
    if ($("#addForm").valid()) {
        $('#submitContactBtn').prop('disabled', true);
        var formData = new FormData();
        formData.append("Name", $('#contactName').val());
        formData.append("Surname", $('#contactSurname').val());
        formData.append("Email", $('#contactEmail').val());
        $.ajax({
            type: "POST",
            data: formData,
            url: "/Contact/Add",
            traditional: true,
            contentType: false,
            processData: false,
            success: function (result) {
                refreshContactPage();
                $('#addModal').modal('close');
                $('#submitContactBtn').prop('disabled', false);
                M.toast({ html: 'The contact has successfully been added.' });
            }
        });
    }
});
$('#submitClientBtn').click(function () {
    if ($("#addForm").valid()) {
        $('#submitClientBtn').prop('disabled', true);
        var formData = new FormData();
        formData.append("Name", $('#clientName').val());
        $.ajax({
            type: "POST",
            data: formData,
            url: "/Client/Add",
            traditional: true,
            contentType: false,
            processData: false,
            success: function (result) {
                refreshClientPage();
                $('#addModal').modal('close');
                $('#submitClientBtn').prop('disabled', false);
            }
        });
    }

});
function linkContact(clientId) {
    var selectedValues = $('#contactSelect').val();
    if ($('#contactSelect option:selected').length > 0) {
        $('#submitClientBtn').prop('disabled', true);
        var formData = new FormData();
        formData.append("ClientId", clientId);
        formData.append("SelectedContacts[]", selectedValues);
        $.ajax({
            type: "POST",
            data: formData,
            url: "/Link/LinkContacts",
            traditional: true,
            contentType: false,
            processData: false,
            success: function (result) {
                $('#linkModal').modal('close');
                window.location.href = result.redirectToUrl;
                M.toast({ html: 'The client has successfully been added.' });
            }
        });
    }

};
function linkClient(contactId) {
    var selectedValues = $('#clientSelect').val();
    if ($('#clientSelect option:selected').length > 0) {
        $('#submitClientBtn').prop('disabled', true);
        var formData = new FormData();
        formData.append("ContactId", contactId);
        formData.append("SelectedClients[]", selectedValues);
        $.ajax({
            type: "POST",
            data: formData,
            url: "/Link/LinkClients",
            traditional: true,
            contentType: false,
            processData: false,
            success: function (result) {
                $('#linkModal').modal('close');
                window.location.href = result.redirectToUrl;
                //refreshLinkedClients(contactId);
            }
        });
    }

};
function unlinkContact(clientId, contactId) {
    $.ajax({
        type: "GET",
        url: "/Link/UnlinkContact?clientId=" + clientId + "&contactId=" + contactId,
        traditional: true,
        success: function (result) {
            window.location.href = result.redirectToUrl;
            //refreshLinkedClients(clientId);
        }
    });
}
function unlinkClient(contactId, clientId) {
    $.ajax({
        type: "GET",
        url: "/Link/UnlinkClient?contactId=" + contactId + "&clientId=" + clientId,
        traditional: true,
        success: function (result) {
            window.location.href = result.redirectToUrl;
            //refreshLinkedContact(contactId);
        }
    });
}
function DeleteContact(id) {
    $.ajax({
        type: "GET",
        url: "/Contact/Delete?id=" + id,
        traditional: true,
        success: function (result) {
            window.location.href = result.redirectToUrl;
        }
    });
};
function DeleteClient(id) {
    $.ajax({
        type: "GET",
        url: "/Client/Delete?id=" + id,
        traditional: true,
        success: function (result) {
            window.location.href = result.redirectToUrl;
        }
    });
};
