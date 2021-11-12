// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const animals = [
    { name: 'Nemo', species: 'Fish', class: { name: 'Invertebrata' } },
    { name: 'Simba', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Dory', species: 'Fish', class: { name: 'Invertebrata' } },
    { name: 'Panther', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Rocky', species: 'Bird', class: { name: 'Aves' } },
    { name: 'Diego', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Sid', species: 'Sloth', class: { name: 'Mamalia' } }
]

/*var newCat = [];
var cetak =  function  (e) {
    if (e.species == "fish") {
        e.class.name = "non-mamalia";
    } else {
        e.class.name = "mamalia";
        newCat.push(e);
    }
    console.log(e);
}
*//*//*Tugas 1*//*
    animals.forEach(cetak);
 *//*//*Tugas 2*//*
console.log(newCat);

*/

const onlyCat = [];
animals.forEach(aFungsi);

function aFungsi(item) {
    //tugas1
    if (item.species == "fish") {
        item.class.name = "non-mamalia";
    }
    //tugas2
    else if (item.species == "cat") {
        onlyCat.push(item);
    }
}
console.log(animals);
console.log(onlyCat);

















/*
animals.forEach(aFungsi);

function aFungsi(item, index, arr) {
    if (item.species == "fish") {
        item.class.name = "non-mamalia";
    }
}

console.log(animals);
*/
/*var newCat = [];
var newFish = [];
var cetak = function (e) {
    console.log(e);
}
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species  == "fish") {
        animals[i].class.name = "non-mamalia";
        newFish.push(animals[i]);
    } else {
        animals[i].class.name = "mamalia";
        newCat.push(animals[i]);
    }
}
*//*Tugas 1*//*
animals.forEach(cetak);
 *//*Tugas 2*//*
console.log(newCat);
console.log(newFish);
*/