// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//let array = [1, 2, 3, 4];
//console.log(array);

//const animals = [
//    { name: "garfield", species: "cat", class: { name: "mamalia" } },
//    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
//    { name: "tom", species: "cat", class: { name: "mamalia" } },
//    { name: "garry", species: "cat", class: { name: "mamalia" } },
//    { name: "dory", species: "fish", class: { name: "invertebrata" } }
//]
//console.log(animals);
//const OnlyCat = [];

//for (var i = 0; i < animals.length; i++) {
//    if (animals[i].species == "fish") {
//        animals[i].class.name = "Non-Mamalia";
//    }
//}
//console.log(animals);

//for (var i = 0; i < animals.length; i++) {
//    if (animals[i].species == "cat") {
//        OnlyCat.push(animals[i]);
//    }
//}

//console.log(OnlyCat);


//$.ajax({
//    url: "https://swapi.dev/api/people/",
//    success: function (result) {
//        console.log(result);

//        var text = "";
//        $.each(result.results, function (key, val) {
//            text += `<tr>
//                        <th>${key+1}</th>
//                        <th>${val.name}</th>
//                        <th>${val.height}</th>
//                        <th>${val.gender}</th>
//                        <th>${val.mass}</th>
//                        <th>${val.hair_color}</th>
//                    </tr>`;
//        });
//        console.log(text);
//        $("#table").html(text);
//    }
//})

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon?limit=1000&offset=1",
    success: function (result) {
        console.log(result);

        var text = "";
        $.each(result.results, function (key, val) {
            text += `<tr>
                        <td>${key + 1}</td>
                        <td>${val.name}</td>
                        <td><button type="button" class="btn btn-primary" data-toggle="modal" onclick="detail('${val.url}')" data-target="#exampleModal">Detail</button></td>
                    </tr>`;
        });
        console.log(text);
        $("#table").html(text);
    }
})

function detail(url) {
    $.ajax({
        url: url,
        success: function (result) {
            console.log(result);
            $('.picture').attr('src', `${result.sprites.other['official-artwork'].front_default}`)
            $('.name').html(result.name)

            $('.type').html(result.types[0].type.name)
            $('.type2').html(result.types[1].type.name)
            $('.abilities1').html(result.abilities[0].ability.name)
            $('.abilities2').html(result.abilities[1].ability.name)
            $('.hp').css({ "width": `${result.stats[0].base_stat}%` })
            $('.hppoint').html(result.stats[0].base_stat)
            $('.ap').css({ "width": `${result.stats[1].base_stat}%` })
            $('.appoint').html(result.stats[1].base_stat)
            $('.dp').css({ "width": `${result.stats[2].base_stat}%` })
            $('.dppoint').html(result.stats[2].base_stat)
            $('.sp').css({ "width": `${result.stats[5].base_stat}%` })
            $('.sppoint').html(result.stats[5].base_stat)
            $('.height').html(result.height)
            $('.weight').html(result.weight)
        }
    })
}

$(document).ready(function () {
    $('#table_id').DataTable();
});

