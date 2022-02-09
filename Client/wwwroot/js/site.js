// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//console.log("ganti clas name");

//let animals = [
//    { name: "dory", species: "fish", class: { name: "invertebrata" } },
//    { name: "simba", species: "cat", class: { name: "mamalia" } },
//    { name: "tori", species: "cat", class: { name: "mamalia" } },
//    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
//    { name: "budi", species: "cat", class: { name: "mamalia" } }
//];

//for (i = 0; i < animals.length; i++) {
//    if (animals[i].species.match("fish")) {
//        animals[i].class.name = "Non-Mamalia";
//    };

//}
//console.log(animals);

////
//let cats = [];
//for (i = 0; i < animals.length; i++) {
//    if (animals[i].species.match("cat")) {
//        cats.push(animals[i]);
//    };

//}
//console.log("push ke cats");
//console.log(cats);
//animals.forEach();

//$.ajax({
//    url: "https://swapi.dev/api/people",
//    success: function (result) {
//        console.log(result.results);
//        var text = "";
//        var no = 0;
       
//        $.each(result.results, function (key, val) {
//            text += `<tr>
//                            <th scope="row">${no + 1}</th>
//                            <td>${val.name}</td>
//                            <td>${val.height}</td>
//                            <td>${val.mass}</td>
//                            <td>${val.hair_color}</td>
//                            <td>${val.skin_color}</td>
//                            <td>${val.eye_color}</td>
//                            <td>${val.birth_year}</td>
//                            <td>${val.gender}</td>
//                         </tr>`;
//            no++;
//        });
//       // console.log(text);
//        $('.tableSW').html(text);
//    }
//});

$(document).ready(function () {
    
    console.log("ww");
    var no = 1;
    $('#tableEmployee').dataTable({
        
        "buttons": [
            'copy', 'excel', 'pdf', 'csv'
        ],
        "ajax": {
            "url": "https://localhost:44316/api/Employees/registeredData",
            "dataType": "json",
            "dataSrc": "result",
        },
        "columns": [

            {
                "data": null,
                render: function (data, type, row) {
                    return `<td class="text-center">${no++}</td>`
                }
            },
            { "data": "fullName" },
            {
                "data": null,
                render: function (result) {


                    //phone = `+62${result.phone}`;
                    result.phone.split('');
                    //[...result.phone];
                    if (result.phone[0] == 0 && result.phone[1] == 8) {
                        //var p = result.phone.splice(0, 2);
                        //p1 = result.phone.shift();
                        phone = `+62${result.phone}`
                        return phone
                    }
                    

                    return phone;
                }
            },
            {
                "data": null,
                render: function (result) {
                    salary = `Rp.${result.salary},00`;
                    return salary
                }
            },
            { "data": "email" },
            { "data": "degree" },
            { "data": "role" },
            {
                "data": null,
                render: function (data, type, row) {
                        return `<button class="btn btn-secondary" data-target="#detailModal" data-toggle="modal">Detail</button>`;  
                },
                "orderable": "false",
            },
            
        ],
        "select": true,
        "colReorder": true,

            
        

    });
    //console.log(a);
    console.log("dtbal")
});


function createEmployee() {
    
    var obj = {};
    obj.FirstName = $('#firstName').val();
    obj.LastName = $('#lastName').val();
    obj.PhoneNumber = $('#phone').val();
    obj.birthDate = $('#birthDate').val();
    obj.salary = $('#salary').val();
    obj.email = $('#email').val();
    obj.degree ="Chemistry",
    obj.GPA = "4",
    obj.password = "test",
    obj.universityId = 2,
    console.log(obj);
    $.ajax({ 
        url: "https://localhost:44316/api/Employees/RegisterVM",
        type: "POST",
        data: JSON.stringify(obj),
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done((result) => {
        window.alert("done");
    }).fail((error) => {

        window.alert("gagal");
        //alert pemberitahuan jika gagal
    });
}