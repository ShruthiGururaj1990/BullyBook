var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    //debugger;
    //dataTable = $('#tblData').DataTable({
    //    "ajax": {
    //        "url" : "/Admin/Product/GetAll"
    //    },
    //    "columns": [
    //        { "data": "Title", "width": "15%" },
    //        { "data": "ISBN", "width": "15%" },
    //        { "data": "author", "width": "15%" },
    //      /*  { "data": "Author", "width": "15%" },*/
    //    ]

    $.ajax({
        type: "GET",
        url: "https://localhost:44375/Admin/Product/GetAll",
        dataType: "json",
        success: function (result, status, xhr) {
            // $(document).getElementById('#tblData') = $("<table><tr><th>Weather Description</th></tr>");

            result.data.forEach(iterate);
            
        }
    });

    function iterate(res) {
        var table = document.getElementById('tblBody');
        //var res = result.data[0]

        let row = document.createElement("tr");
        // Create cells
        let c1 = document.createElement("td")
        let c2 = document.createElement("td")
        let c3 = document.createElement("td")
        let c4 = document.createElement("td")

        // Insert data to cells
        c1.innerText = res["title"]
        c2.innerText = res["isbn"]
        c3.innerText = res["price"]
        c4.innerText = res["author"]

        // Append cells to row
        row.appendChild(c1);
        row.appendChild(c2);
        row.appendChild(c3);
        row.appendChild(c4);

        table.appendChild(row);
    }

}