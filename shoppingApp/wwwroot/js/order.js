var dataTable;

$(document).ready(function () {
    loadDataTable();
}); 

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/order/GetAll' },
        "columns": [
            { data: 'Id', "width": "25%" },
            { data: 'Name', "width": "15%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'ApplicationUser.Email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'OrderTotal', "width": "10%" },
            {
                data: 'category.name',
                render: function (data, type, row) {
                    return row.category ? row.category.name : 'N/A';
                },
                width: "10%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>                               },
                "width": "25%"
            }
        ]
    });
}
