var userTable;
var i = 1;
var nameSearchString, emailSearchString, fromSearchString, toSearchString;

$(document).ready(function () {
    drawDataTable('','','','');

    $("#fromSearchString").datepicker({ minDate: -20, maxDate: "+1M +15D", format: 'yyyy/mm/dd' });
    $("#toSearchString").datepicker({ minDate: -20, maxDate: "+1M +15D", format: 'yyyy/mm/dd' });

});

function getDetails(id) {
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    $.ajax({
        url: "/User/Details?id=" + id,
        type: 'get',
        dataType: "json",
        success: function (data, status) {
            //console.log(data);
            $("#detailName").text(data.name);
            $("#detailType").text(data.type);
            $("#detailEmail").text(data.email);
            $("#detailPhone").text(data.phone);
            $("#detailDOB").text(data.dob == "" ? "No DoB filled." : data.dob);
            $("#detailAddress").text(data.address == "" ? "No address filled." : data.address);
            $("#detailCreatedDate").text(data.createdDate);
            $("#detailCreatedUser").text(data.createdUser);
            $("#detailUpdatedDate").text(data.updatedDate == "" ? "Have not been updated." : data.updatedDate);
            $("#detailUpdatedUser").text(data.updatedUser == "" ? "Have not been updated." : data.updatedUser);
            if (data.profile != null) {
                $("#detailProfile").attr("src", "data:image/jpg;base64," + data.profile);
            }
            //$("#detailProfile").attr("width", "100%");
            //$("#detailProfile").attr("height", "auto");
            $('#detailsModal').modal('show');
        }
    });
}

function drawDataTable(nameSearchString, emailSearchString, fromSearchString, toSearchString) {
    userTable =
        $('#userTable').DataTable({
            processing: true,
            ordering: true,
            paging: true,
            searching: true,
            responsive: true,
            ajax: "User/GetUsers?nameSearchString=" + nameSearchString + "&emailSearchString=" + emailSearchString + "&fromSearchString=" + fromSearchString + "&toSearchString=" + toSearchString,
            columnDefs: [
                { className: "dt-center", targets: [0, 1, 2, 3, 4, 5, 6, 7, 8] }
            ],
            columns: [
                {
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
                { data: "id", render: function (data, type, row) { return '<button type="button" class="link-button" onclick="getDetails(\'' + data + '\')" data-toggle="modal" data-target="#detailsModal"> ' + row.name + '</button>' } },
                { data: "email" },
                { data: "createdUser" },
                { data: "type" },
                { data: "phone" },
                { data: "dob" },
                { data: "address" },
                { data: "id", render: function (data, type, row) { return '<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#deleteModal">Delete</button>' } }
            ]
        });
}

function search() {
    nameSearchString = $('#nameSearchString').val();
    emailSearchString = $('#emailSearchString').val();

    fromSearchString = $('#fromSearchString').val();
    if (fromSearchString != null && fromSearchString != '') {
        var fromDate = new Date(Date.parse(fromSearchString));
        fromSearchString = $.datepicker.formatDate('yy-mm-dd', fromDate);
    }

    toSearchString = $('#toSearchString').val();
    if (toSearchString != null && toSearchString != '') {
        var toDate = new Date(Date.parse(toSearchString));
        toSearchString = $.datepicker.formatDate('yy-mm-dd', toDate);
    }

    if (userTable) {
        userTable.destroy();
    }
    drawDataTable(nameSearchString, emailSearchString, fromSearchString, toSearchString);
}

$('#userTable').on('click', 'tbody tr .btn-danger', function () {
    var table = $('#userTable').DataTable();
    var tr = $(this).closest('tr');
    var data = table.row(tr).data();
    showDeleteModal(data);
});

function showDeleteModal(data) {
    $("#deleteId").text(data.id);
    $("#deleteName").text(data.name);
    $("#deleteType").text(data.type);
    $("#deleteEmail").text(data.email);
    $("#deletePhone").text(data.phone);
    $("#deleteDOB").text(data.dob == "" ? "No DoB filled." : data.dob);
    $("#deleteAddress").text(data.address == "" ? "No address filled." : data.address);
    $("#deleteModalFooter a").attr("href", "/Post/Delete?id=" + data.id)
    $('#deleteModal').modal('show');
}