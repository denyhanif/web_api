$(document).ready(function () {
    var no = 1;
    $('#tableSW').dataTable({
        "ajax": {
            "url": "https://pokeapi.co/api/v2/pokemon/",
            "dataType": "json",
            "dataSrc": "results",
        },
        "columns": [

            {
                "data": null,
                render: function (data, type, row) {
                    return `<td class="text-center">${no++}</td>`
                }
            },
            { "data": "name" },
            {
                "data": null,
                render: function (data, type, row) {
                    return `<button class="btn btn-secondary" onclick="detailSW('${row['url']}')" data-target="#detailModal" data-toggle="modal">Detail</button>`;
                }
            }
        ]
    });
    console.log("dtbal")
});

//function Detail(url) {
//    ability = "";
//    badge = "";
//    stat = "";
//    move = "";
//    $.ajax({
//        url: url,
//        success: function (result) {
//            result.abilities.forEach(ab => {
//                ability += `${ab.ability.name}\n`
//            });
//            result.moves.forEach(mo => {
//                move += `<li>${mo.move.name}</li><>`
//            })
//            result.stats.forEach(st => {

//                stat += `<tr>
//                            <td>${st.stat.name}</td>
//                            <td class="col-8">
//                                <div class="progress">
//                                  <div class="progress-bar progress-bar-striped" role="progressbar" style="width: ${st.base_stat}%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100">${st.base_stat}</div>
//                                </div>
//                            </td>  
//                        </tr>`
//            });
//            result.types.forEach(t => {
//                type = `${t.type.name}`
//                if (type == "poison") {
//                    badge += `<span class="badge badge-pill badge-dark text-light m-2">${type}</span>`
//                }
//                else if (type == "grass") {
//                    badge += `<span class="badge badge-pill badge-success m-2">${type}</span>`
//                }
//                else if (type == "normal") {
//                    badge += `<span class="badge badge-pill badge-secondary m-2">${type}</span>`
//                }
//                else if (type == "fire") {
//                    badge += `<span class="badge badge-pill badge-danger m-2">${type}</span>`
//                }
//                else if (type == "water") {
//                    badge += `<span class="badge badge-pill badge-primary m-2">${type}</span>`
//                }
//                else if (type == "bug") {
//                    badge += `<span class="badge badge-pill badge-info m-2">${type}</span>`
//                }
//                else if (type == "flying") {
//                    badge += `<span class="badge badge-pill badge-muted m-2">${type}</span>`
//                }
//            });
//            console.log(result);
//            text = "";
//            text = `
//                    <div class="row">
//                        <div class="col-md-6">
//                            <div class="modal-image">
//                                <img src="${result.sprites.other.dream_world.front_default}" alt="Alternate Text" style="width:100%" />
//                            </div>
//                            <div class="type">
//                                ${badge}
//                            </div>

//                        </div>
//                        <div class="col-md-6 ">
//                            <div class="row detail">
//                                <table class="table table-striped p-1">
//                                    <thead>
//                                        <tr>
//                                            <th class="th-detail text-center" colspan="2">Detail</th>
//                                        </tr>
//                                    </thead>
//                                    <tbody>
//                                        <tr>
//                                            <td>Name:</td>
//                                            <td>${result.name}</td>
//                                        </tr>
//                                        <tr>
//                                            <td>Ability:</td>
//                                            <td>${ability}</td>
//                                        </tr>
//                                        <tr>
//                                            <td>Weight:</td>
//                                            <td>${result.weight}</td>
//                                        </tr>
//                                        <tr>
//                                            <td>Height:</td>
//                                            <td>${result.height}</td>
//                                        </tr>

//                                    </tbody>
//                                </table>
//                            </div>

//                        </div>
//                    </div>
//                    <div class="row ">
//                        <ul class="nav nav-tabs" id="myTab" role="tablist">
//                            <li class="nav-item">
//                                <a class="nav-link active" id="detail-tab" data-toggle="tab" href="#detail" role="tab" aria-controls="home" aria-selected="true">Detail</a>
//                            </li>
//                            <li class="nav-item">
//                                <a class="nav-link" id="stat-tab" data-toggle="tab" href="#stat" role="tab" aria-controls="profile" aria-selected="false">Stat</a>
//                            </li>
//                            <li class="nav-item">
//                                <a class="nav-link" id="move-tab" data-toggle="tab" href="#move" role="tab" aria-controls="profile" aria-selected="false">Moves</a>
//                            </li>
//                        </ul>
//                        <div class="tab-pane fade show active" id="detail" role="tabpanel" aria-labelledby="home-tab">
//                            <table class="table table-hover table-striped">
//                                <tr>
//                                    <td>Name</td>
//                                    <td class="col-8">: ${result.name}</td>
//                                </tr>

//                            </table>
//                        </div>
//                        <div class="tab-pane fade" id="stat" role="tabpanel" aria-labelledby="profile-tab">
//                            <table class="table table-hover ">
//                                ${stat}
//                            </table>
//                        </div>
//                        <div class="tab-pane fade" id="move" role="tabpanel" aria-labelledby="move-tab">
//                            <table class="table table-hover ">
//                                <ol>
//                                    ${move}
//                                    <ol>
//                            </table>
//                        </div>

//                    </div>
//            `
//            $('.modal-body').html(text);

//        }
//    });
//}

function detailSW(url) {
    ability = "";
    badge = "";
    stat = "";
    move = "";
    $.ajax({
        url: url,
        success: function (result) {
            result.abilities.forEach(ab => {
                ability += `${ab.ability.name}\n`
            })
            result.moves.forEach(mo => {
                move += `<li>${mo.move.name}</li>`
            })
            result.stats.forEach(st => {
                 
                stat += `<tr>
                            <td>${st.stat.name}</td>
                            <td class="col-8">
                                <div class="progress">
                                  <div class="progress-bar bg-warning" role="progressbar" style="width: ${st.base_stat}%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100">${st.base_stat}</div>
                                </div>
                            </td>  
                        </tr>`
            })
            result.types.forEach(t => {
                type = `${t.type.name}`
                if (type == "poison") {
                    badge += `<span class="badge badge-pill badge-dark text-light m-2">${type}</span>`
                }
                else if (type == "grass") {
                    badge += `<span class="badge badge-pill badge-success m-2">${type}</span>`
                }
                else if (type == "normal") {
                    badge += `<span class="badge badge-pill badge-secondary m-2">${type}</span>`
                }
                else if (type == "fire") {
                    badge += `<span class="badge badge-pill badge-danger m-2">${type}</span>`
                }
                else if (type == "water") {
                    badge += `<span class="badge badge-pill badge-primary m-2">${type}</span>`
                }
                else if (type == "bug") {
                    badge += `<span class="badge badge-pill badge-info m-2">${type}</span>`
                }
                else if (type == "flying") {
                    badge += `<span class="badge badge-pill badge-muted m-2">${type}</span>`
                }
            });
            console.log(type);
            text = "";
            text = `
                    <div class="row">
                        <div class="col-md-6">
                            <div class="modal-image">
                                <img src="${result.sprites.other.dream_world.front_default}" alt="Alternate Text" style="width:100%" />
                            </div>
                            <div class="type">
                                ${badge}
                            </div>

                        </div>
                        <div class="col-md-6 ">
                            <div class="row detail">
                                <table class="table table-striped p-1">
                                    <thead>
                                        <tr>
                                            <th class="th-detail text-center" colspan="2">Detail</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Name:</td>
                                            <td>${result.name}</td>
                                        </tr>
                                        <tr>
                                            <td>Ability:</td>
                                            <td>${ability}</td>
                                        </tr>
                                        <tr>
                                            <td>Weight:</td>
                                            <td>${result.weight}</td>
                                        </tr>
                                        <tr>
                                            <td>Height:</td>
                                            <td>${result.height}</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                      <li class="nav-item">
                        <a class="nav-link text-dark h5" id="stat-tab" data-toggle="tab" href="#stat" role="tab" aria-controls="home" aria-selected="true">Stat</a>
                      </li>
                      <li class="nav-item">
                        <a class="nav-link text-dark h5" id="move-tab" data-toggle="tab" href="#move" role="tab" aria-controls="profile" aria-selected="false">Moves</a>
                      </li>
                    </ul>
                    <div class="tab-content" id="myTabContent">
                        <div class="tab-pane fade show active" id="stat" role="tabpanel" aria-labelledby="home-tab">
                            <table class="table table-hover ">
                                ${stat}
                            </table>
                        </div>
                        
                        <div class="tab-pane fade" id="move" role="tabpanel" aria-labelledby="move-tab">
                            
                                <ol>
                                    ${move}
                                <ol>
                                    
                        </div>
                    </div>
                   `
            $('.modal-body').html(text);
        }
    })
}