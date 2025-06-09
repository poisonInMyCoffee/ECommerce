

$(document).ready(function () {
    loadDataTable();
}); 

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {url:'/Admin/Product/GetAll'},
        "columns": [
            { data: 'title',"width":"15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'author', "width": "15%" },
            {
                data: 'category.name',
                render: function (data, type, row) {
                    return row.category ? row.category.name : 'N/A';
                },
                width: "15%"
            }
        ]
    });
}

