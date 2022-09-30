var searchString;
var postTable;

$(document).ready(function () {
    drawDataTable("");
});

function drawDataTable(searchString) {
    postTable = $('#postTable').DataTable({
                    processing: true,
                    ordering: true,
                    paging: true,
                    searching: true,
                    ajax: "Post/GetPosts?searchString=" + searchString,
                    columnDefs: [
                        { className: "dt-center", targets: [0, 1, 2, 3, 4] }
                    ],
                    columns: [
                        { data: "title" },
                        { data: "description" },
                        { data: "postedUser" },
                        { data: "postedDate" },
                        { data: "id", render: function (data) { return '<a class="btn btn-primary" href="/Post/Edit?id=' + data + '">Edit</a><a class="btn btn-danger" style="margin-left: 10px;" href="/Post/Delete?id=' + data + '">Delete</a>' } }
                    ]
                });
}

function search() {
    var searchString = $('#searchString').val();

    if (postTable) {
        postTable.destroy();
    }
    drawDataTable(searchString);
}