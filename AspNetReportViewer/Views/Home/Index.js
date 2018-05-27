$(function () {
    function ReloadDataTable() {
        $('#DataTable').puidatatable({
            columns: [
                { field: 'Name', headerText: 'Nome', bodyStyle: 'text-align:center', editor: 'input' },
                { field: 'Age', headerText: 'Idade', bodyStyle: 'text-align:center', editor: 'input' },
                { field: 'SocialCode', headerText: 'CPF', bodyStyle: 'text-align:center', editor: 'input' },
            ],
            editMode : 'cell',
            datasource: function (callback) {
                $.ajax({
                    type: "GET",
                    url: 'Home/GetData',
                    dataType: "json",
                    context: this,
                    success: function (response) {
                        callback.call(this, response);
                    }
                });
            },
            selectionMode: 'single'
        });
    }

    ReloadDataTable();

    $('#GenerateReport').click(function (e) {
        e.preventDefault();

        var Databody = $('#DataTable').find('tbody tr');
        for (var i = 0; i < Databody.length; i++) {
            $('#DataTable').puidatatable('selectRow', $(Databody[i]));
        }
        $.ajax({
            url: '/Home/GenerateReport',
            data: JSON.stringify($('#DataTable').puidatatable('getSelection')),
            contentType: 'application/json',
            type: 'POST',
            success: function (url) {
                window.open('/Content/Temp/' + url);
            },
            error: function (e) {
                alert("Houve um erro ao gerar o relatório");
            }
        })
        $('#DataTable').puidatatable('unselectAllRows');
    });
})