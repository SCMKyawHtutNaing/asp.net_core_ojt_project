var searchString;
var postTable;

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
            $("#detailUpdatedDate").text(data.updatedDate);
            $("#detailUpdatedUser").text(data.updatedUser);
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
        $('#postTable').DataTable({
            processing: true,
            ordering: true,
            paging: true,
            searching: true,
            ajax: "Post/GetPosts?searchString=" + searchString,
            columnDefs: [
                { className: "dt-center", targets: [0, 1, 2, 3, 4] }
            ],
            columns: [
                { data: "id", render: function (data, type, row) { return '<button type="button" class="link-button" onclick="getDetails(' + data + ')" data-toggle="modal" data-target="#detailsModal"> ' + row.title + '</button>' } },
                { data: "description" },
                { data: "createdUser" },
                { data: "createdDate" },
                { data: "id", render: function (data, type, row) { return '<a class="btn btn-primary" href="/Post/Edit?id=' + data + '">Edit</a><button type="button" class="btn btn-danger" style="margin-left: 5px;" data-toggle="modal" data-target="#deleteModal">Delete</button>' } }
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