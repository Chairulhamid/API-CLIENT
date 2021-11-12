// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon/",
    success: function (result) {

        var ajakPeople = "";
        $.each(result.results,  function (key, value) {
            ajakPeople += `<tr>
                                    <td scope=" row">${key+1}</td>
                                    <td scope=" row">${value.name}</td>
                             
                                    <td scope=" row">  <a class="btn btn-primary btn-sm text-light" data-url="${value.url}" onclick="launchModal('${value.url}');" data-toggle="modal" data-target="#modalPoke"">Detail</a></td>
                                </tr>`;
        });
        $('#isiData').html(ajakPeople);
        }
    })
function launchModal(url) {
    console.log(url);
    listPoke = "";
    $.ajax({
        url: url,
        success: function (result) {
            var type = "";
                for (var i = 0; i < result.types.length; i++) {
                if (result.types[i].type.name == 'grass') {
                    type += `<span class="badge badge-pill badge-primary ml-5">Grass</span>`;
                } else if (result.types[i].type.name == 'poison') {
                    type += `<span class="badge badge-pill badge-secondary ml-5">Poison</span>`;
                } else if (result.types[i].type.name == 'fire') {
                    type += `<span class="badge badge-pill badge-success ml-5">Fire</span>`;
                } else if (result.types[i].type.name == 'water') {
                    type += `<span class="badge badge-pill badge-danger ml-5">Water</span>`;
                } else if (result.types[i].type.name == 'bug') {
                    type += `<span class="badge badge-pill badge-warning ml-5">Bug</span>`;
                } else {
                    type += `<span class="badge badge-pill badge-info ml-5">Flying</span>`;
                }
            };

            var move = "";
            var a = result.moves;
            for (var a = 0; a < 10 ; a++) {
                move +=`<input type="text" readonly class="form-control-plaintext ml-5 font-weight-bold " value="${result.moves[a].move.name}">`;
            }

            var ability = "";
            for (var i = 0; i < result.abilities.length ; i++) {
                ability += `<input type="text" readonly class="form-control-plaintext ml-5 font-weight-bold " id="staticEmail" value="${result.abilities[i].ability.name}">`;
            }
          var stat = "";
            for (var i = 0; i < result.stats.length; i++) {
                stat += `<input type="text" readonly class="form-control-plaintext ml-5 font-weight-bold " id="staticEmail" value="${result.stats[i].stat.name}">`;
            }

            listPoke +=
                `<div class="row no-gutters justify-content-center ">
                    <div class="col-lg-7 mb-3  justify-content-center">
                        <img class=" img-fluid rounded-circle bg-secondary img-thumbnail mb-2" src="${result.sprites.other.dream_world.front_default}" width="500">
                    </div>
                    <div class="container col-lg-12 mb-1  text-center">
                        <h4 class="text-uppercase  font-weight-bold  ">${result.name}</h4>
                    </div>
                </div>
                <div class="container">
                    <div class="form-group row ml-5">
                        <label for="staticEmail" class="col-sm-2 col-form-label font-weight-bold">Type</label>
                        <div class="col-sm-10">
                            ${type}
                        </div>
                    </div>
                     <div class="form-group row ml-5">
                        <label for="staticEmail" class="col-sm-2 col-form-label font-weight-bold">Weight</label>
                        <div class="col-sm-10">
                            <input type="text" readonly class="form-control-plaintext ml-5 font-weight-bold" id="staticEmail" value="${result.weight} Kg.">
                        </div>
                    </div>
                    <div class="form-group row ml-5">
                        <label for="staticEmail" class="col-sm-2 col-form-label font-weight-bold">Abilities</label>
                        <div class="col-sm-10">
                               ${ability}
                        </div>
                    </div>
                        <div class="form-group row ml-5">
                        <label for="staticEmail" class="col-sm-2 col-form-label font-weight-bold">Moves</label>
                        <div class="col-sm-10">
                            ${move}
                        </div>
                    </div>
                     <div class="form-group row ml-5">
                        <label for="staticEmail" class="col-sm-2 col-form-label font-weight-bold">Stats</label>
                        <div class="col-sm-10">
                            ${stat}
                        </div>
                    </div>
                </div>`;

            $('.modal-body').html(listPoke);
        }
    });
}