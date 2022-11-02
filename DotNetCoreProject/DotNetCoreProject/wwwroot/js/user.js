var searchString;
var postTable;
var i = 1;

$(document).ready(function () {
    drawDataTable("");
});

function getDetails(id) {
    $.get("/Post/Details", { id: id },
        function (data, status) {
            $("#detailTitle").text(data.title);
            $("#detailDescription").text(data.description);
            $("#detailStatus").text(data.status ? "Active" : "Inactive");
            $("#detailCreatedDate").text(data.createdDate);
            $("#detailCreatedUser").text(data.createdUser);
            $("#detailUpdatedDate").text(data.updatedDate == "" ? "Have not been updated." : data.updatedDate);
            $("#detailUpdatedUser").text(data.updatedUser == "" ? "Have not been updated." : data.updatedUser);
            $('#detailsModal').modal('show');
        });
}

function showDeleteModal(data) {
    $("#deleteTitle").text(data.title);
    $("#deleteDescription").text(data.description);
    $("#deleteStatus").text(data.status ? "Active" : "Inactive");
    $("#deleteModalFooter a").attr("href", "/Post/Delete?id=" + data.id)
    $('#deleteModal').modal('show');
}

function drawDataTable(searchString) {
    postTable =
        $('#userTable').DataTable({
            processing: true,
            ordering: true,
            paging: true,
            searching: true,
            ajax: "User/GetUsers?searchString=" + searchString,
            columnDefs: [
                { className: "dt-center", targets: [0, 1, 2, 3, 4, 5, 6, 7] }
            ],
            columns: [
                {
                    "render": function (data, type, full, meta) {
                        return i++;
                    }
                },
                { data: "name" },
                { data: "email" },
                { data: "createdUser" },
                { data: "type" },
                { data: "phone" },
                { data: "dob" },
                { data: "address" }
            ]
        });
}

$('#postTable').on('click', 'tbody tr .btn-danger', function () {
    var table = $('#postTable').DataTable();
    var tr = $(this).closest('tr');
    var data = table.row(tr).data();
    showDeleteModal(data);
});

function search() {
    var searchString = $('#searchString').val();

    if (postTable) {
        postTable.destroy();
    }
    drawDataTable(searchString);
}