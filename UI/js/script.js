window.addEventListener('load', (event) => {

    if (!Boolean(sessionStorage.getItem("jwt"))) {
        window.location.href = "../Login.html";
    }
    const baseUrl = 'http://localhost:51740/api/Breeds/';

    function fetchBreeds() {
        fetch(baseUrl, {
                headers: {
                    "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
                }
            })
            .then(response => {
                if (response.status === 200) {
                    return response.json();
                } else {
                    console.log("something wrong happened");
                    return response.json()
                }
            })
            .then((data) => {
                var BreedHTMLListMapped = data.map(p =>
                    `<div class="col-md-6">
                    <a href="breed.html?id=${p.id}">
                        <div class="image">
                            <img src="images/${p.name === "Undeads" | p.name === "Orcs" | p.name === "Humans" | p.name === "NightElves" ? p.name : "Default"}.jpg" alt="${p.name}">                            
                        </div>
                        <div class="image-title" >
                            <span>${p.name}</span>
                        </div>
                        
                    </a>
                </div>
               `);
                var breedContent = `${BreedHTMLListMapped.join('')}`;
                document.getElementById("breed-list-content").innerHTML = breedContent;
            })
    }

    function fetchPostBreed(event) {
        event.preventDefault();
        var data = {
            name: event.currentTarget.name.value,
            typesOfUnity: parseInt(event.currentTarget.typesOfUnity.value),
            defaultColor: event.currentTarget.defaultColor.value,
            reign: event.currentTarget.reign.value,
            armyName: event.currentTarget.armyName.value,
            difficulty: event.currentTarget.difficulty.value,
            rating: parseFloat(event.currentTarget.rating.value)
        }
        fetch(baseUrl, {
            headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${sessionStorage.getItem("jwt")}` },
            method: 'POST',
            body: JSON.stringify(data)
        }).then((response) => {
            if (response.status === 201) {
                console.log("Breed created successfuly");
            } else {
                response.text().then((data) => {
                    console.log(data);
                });
            }
        }).catch((response) => {
            console.log(data);
        });
    }

    function fetchGetSingleBreed() {

        let stringurl = window.location.href;
        let localid = stringurl.split('=')[1];

        fetch(`${baseUrl}${localid}`, {
                headers: {
                    "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
                }
            })
            .then(response => {
                if (response.status === 200) {
                    return response.json();
                } else {
                    console.log("something wrong happened");
                    return response.json()
                }
            })
            .then(data => {
                var heroesPerBreed = "";
                var SingleBreedHtmlMapped =
                    `   <h2 class="text-center Character-title">${data.name}</h2>
                        <br>
                        <section class="row">
                            <div class="col-md-6">
                                <div class="text-tile">
                                    <h2 class="text-left Character-title">${data.name}</h2>

                                    <section class="row">
                                        <table class="table table-dark" style:"margin-left: 2px;>
                                            <div class="col-md-6">
                                                <thead>
                                                    <tr>
                                                        <th>Key</th>
                                                        <th>Value</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Id</td>
                                                        <td>${data.id}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Name</td>
                                                        <td>${data.name}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>TypesOfUnity</td>
                                                        <td>${ parseInt(data.typesofUnity)}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>DefaultColor</td>
                                                        <td>${data.defaultColor}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Reign</td>
                                                        <td>${data.reign}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>ArmyName</td>
                                                        <td>${data.armyName}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Difficulty</td>
                                                        <td>${data.difficulty}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Rating</td>
                                                        <td>${ parseFloat(data.rating)}</td>
                                                    </tr>
                                                </tbody>
                                            </div>
                                        </table>
                                    </section>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <img width="500px" height="700px" style="margin-bottom: 20px;" src="images/${data.name === "Undeads" | data.name === "Orcs" | data.name === "Humans" | data.name === "NightElves" ? data.name : "Default"}.jpg" alt="${data.name}">
                            </div>
                        </section>     
            `;
                data.heroes.forEach(element => {
                    heroesPerBreed = heroesPerBreed + `
                    <div class="col-md-6">
                        <a href="../heroes/hero.html?Breed=${element.breedId}&id=${element.id}">
                            <div class="image text-center">
                                <img width="200px" height="200px" src="images/${element.name === "Archmage" | element.name === "Blademaster" | element.name === "BloodMage" | element.name === "CryptLord"| element.name === "DeathKnight" | element.name === "DemonHunter" | element.name === "DreadLord"| element.name === "FarSeer" | element.name === "KeeperOfTheGrove" | element.name === "Lich"| element.name === "MountainKing" | element.name === "PriestessOfTheMoon" | element.name === "ShadowHunter" | element.name === "TaurenChieftain" | element.name === "Warden"| element.name === "Paladin" ? element.name : "Default"}.jpg" alt="${element.name}">
                            </div>
                            <div class="image-title-others text-center ">
                                <span>${element.name}</span>
                            </div>
                        </a>
                    </div>
                `;
                });
                
                var HeroesBreed = heroesPerBreed + `
                <div class="col-md-6">
                    <a href="../heroes/CreateHero.html?Breed=${data.id}">
                        <div class="image text-center">
                            <img width="200px" height="200px" src="images/Add.png" alt="addHero">
                        </div>
                        <div class="image-title-others text-center ">
                            <span>Add New Hero</span>
                        </div>
                    </a>
                </div>
                `;
                // var SinglebreedContent = `${SingleBreedHtmlMapped}`;
                // document.getElementById("Singlebreed-list-content").innerHTML = SinglebreedContent;
                document.getElementById("Singlebreed-list-content").innerHTML = SingleBreedHtmlMapped;
                document.getElementById("CreateHeroesDoc").innerHTML = HeroesBreed;
            });
    }

    function fetchCreateFromBreed(event) {

        let stringurl = window.location.href;
        let localid = stringurl.split('=')[1];
        fetch(`${baseUrl}${localid}`, {
                headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${sessionStorage.getItem("jwt")}` }
            })
            .then(response => {
                if (response.status === 200) {
                    return response.json();
                } else {
                    console.log("something wrong happened");
                    return response.json()
                }
            })
            .then(data => {
                var FormCreated =
                    `
                    <div class="col-md-3">
                        <label for="name">Name</label>
                    </div>
                    <div class="col-md-7">
                        <input type="text" name="name" id="form-name" value="${data.name}">
                    </div>
                    <div class="col-md-3">
                        <label for="typesOfUnity">Types Of Unitys</label>
                    </div>
                    <div class="col-md-7">
                        <input type="number" name="typesOfUnity" id="form-typesOfUnity" value=${parseInt(data.typesofUnity)}>
                    </div>
                    <div class="col-md-3">
                        <label for="defaultColor">Default Color</label>
                    </div>
                    <div class="col-md-7">
                        <input type="text" name="defaultColor" id="form-defaultColor" value="${data.defaultColor}">
                    </div>
                    <div class="col-md-3">
                        <label for="reign">Reign</label>
                    </div>
                    <div class="col-md-7">
                        <input type="text" name="reign" id="form-reign" value="${data.reign}">
                    </div>
                    <div class="col-md-3">
                        <label for="armyName">Army Name</label>
                    </div>
                    <div class="col-md-7">
                        <input type="text" name="armyName" id="form-armyName" value="${data.armyName}">
                    </div>
                    <div class="col-md-3">
                        <label for="difficulty">Difficulty</label>
                    </div>
                    <div class="col-md-7">
                        <select name="difficulty" id="form-difficulty">
                            <option value="Easy">Easy</option>
                            <option value="Medium">Medium</option>
                            <option value="Hard">Hard</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <label for="rating">Rating</label>
                    </div>

                    <div class="col-md-7">
                        <input type="number" name="rating" id="form-rating" value=${parseFloat(data.rating)}>
                    </div>
                    <div class="col-md-6" style="text-align : center;">
                        <input type="submit" value="submit">
                    </div>                                
        `;
                var listReady = `<form id="fetch-update-frm" class="row">${FormCreated}</form>`;
                document.getElementById("breed-listready-content").innerHTML = listReady;
                document.getElementById("fetch-update-frm").addEventListener("submit", fetchUpdateBreed);
            });

    }

    function fetchUpdateBreed(event) {
        let stringurl = window.location.href;
        let localid = stringurl.split('=')[1];


        event.preventDefault();

        var data = {
            name: event.currentTarget.name.value,
            typesOfUnity: parseInt(event.currentTarget.typesOfUnity.value),
            defaultColor: event.currentTarget.defaultColor.value,
            reign: event.currentTarget.reign.value,
            armyName: event.currentTarget.armyName.value,
            difficulty: event.currentTarget.difficulty.value,
            rating: parseFloat(event.currentTarget.rating.value)
        }

        fetch(`${baseUrl}${localid}`, {
            headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${sessionStorage.getItem("jwt")}` },
            method: 'PUT',
            body: JSON.stringify(data)
        }).then((response) => {

            if (response.status === 200) {
                console.log("Breed updated successfuly");
            } else {
                response.text().then((data) => {

                    console.log(data);
                });
            }
        }).catch((response) => {
            console.log(data);
        });
    }

    function fetchDeleteBreed(event) {

        let stringurl = window.location.href;
        let idToDelete = stringurl.split('=')[1];


        //var idToDelete = parseInt(event.currentTarget.id.value);
        fetch(`${baseUrl}${idToDelete}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${sessionStorage.getItem("jwt")}` }
        });
    }


    var prueba = document.getElementById("breed-list-content");
    if (prueba) {
        prueba.addEventListener("load", fetchBreeds());
    }
    if (document.getElementById("fetch-frm") != null) {
        document.getElementById("fetch-frm").addEventListener("submit", fetchPostBreed);
    }
    if (document.getElementById("Singlebreed-list-content") != null) {
        document.getElementById("Singlebreed-list-content").addEventListener("load", fetchGetSingleBreed());
    }
    if (document.getElementById("fetch-delete-Btn") != null) {
        document.getElementById("fetch-delete-Btn").addEventListener("click", fetchDeleteBreed);
    }
    if (document.getElementById("fetch-CreateForm-Btn") != null) {
        document.getElementById("fetch-CreateForm-Btn").addEventListener("click", fetchCreateFromBreed);
    }


});