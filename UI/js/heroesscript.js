window.addEventListener('load', (event) => {
    if (!Boolean(sessionStorage.getItem("jwt"))) {
        window.location.href = "../Login.html";
    }
    const baseUrl = 'http://localhost:51740/api/Breeds/';

    function fetchHeroes() {
        let breeds;
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
            }).then((data3) => {
                breeds = data3;
                var breedContent = "";
                breeds.forEach(element => {
                    let i = element.id;
                    var url = baseUrl + i.toString() + "/Heroes"
                    fetch(url, {
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
                                `   <div class="card">
                    <img src="images/${p.name === "Archmage" | p.name === "Blademaster" | p.name === "BloodMage" | p.name === "CryptLord" | p.name === "DeathKnight" | p.name === "DemonHunter" | p.name === "DreadLord" | p.name === "FarSeer" | p.name === "KeeperOfTheGrove" | p.name === "Lich" | p.name === "MountainKing" | p.name === "PriestessOfTheMoon" | p.name === "ShadowHunter" | p.name === "TaurenChieftain" | p.name === "Warden" | p.name === "Paladin" ? p.name : "Default"}.jpg" class="card-img-top" alt="${p.name}" />
                    <div class="card-body">
                        <h5 class="card-title">${p.name}</h5>
                        <p class="card-text">
                            Breed : ${p.breedId}
                            <br>
                            Level : ${p.level}
                            <br>
                            Difficulty : ${p.difficulty}
                        </p>
                    </div>
                    <div class="card-footer">
                        <small class="text-muted">
                            <a href="hero.html?Breed=${p.breedId}&id=${p.id}" style="color: #fad410;">
                                See more ...
                            </a> 
                        </small>
                    </div>
                </div>
               `);
                            breedContent = breedContent + `${BreedHTMLListMapped.join('')}`;
                            document.getElementById("HeroesCards").innerHTML = breedContent;
                        })

                });
            })



    }

    function fetchPostHero(event) {
        let stringurl = window.location.href;
        let localid = stringurl.split('=')[1];
        event.preventDefault();
        var data = {
            name: event.currentTarget.name.value,
            breedId: parseInt(localid),
            hp: parseInt(event.currentTarget.hp.value),
            mana: parseInt(event.currentTarget.mana.value),
            level: parseInt(event.currentTarget.level.value),
            attackType: event.currentTarget.attackType.value,
            difficulty: event.currentTarget.difficulty.value
        }
        var url = baseUrl + data.breedId + "/Heroes"
        fetch(url, {
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            },
            method: 'POST',
            body: JSON.stringify(data)
        }).then((response) => {
            if (response.status === 201) {
                console.log("Hero created successfuly");
            } else {
                response.text().then((data) => {
                    console.log(data);
                });
            }
        }).catch((response) => {
            console.log(data);
        });
    }

    function fetchDeleteHero(event) {
        let stringurl = window.location.href;
        let idBreed = stringurl.split('=')[1].split('&')[0]
        let idHero = stringurl.split('=')[2];
        var url = baseUrl + idBreed + "/Heroes/" + idHero;
        fetch(url, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            }
        });
    }

    function fetchCreateFromHero(event) {

        let stringurl = window.location.href;
        let idBreed = stringurl.split('=')[1].split('&')[0]
        let idHero = stringurl.split('=')[2];
        var url = baseUrl + idBreed + "/Heroes/" + idHero;
        fetch(url, {
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
                var FormCreated =
                    `<div class="col-md-3">
                    <label for="name">Name</label>
                </div>
                <div class="col-md-7">
                    <input type="text" name="name" id="form-name" value="${data.name}">
                </div>
                <div class="col-md-3">
                    <label for="hp">HP</label>
                </div>
                <div class="col-md-7">
                    <input type="number" name="hp" id="form-hp" value=${parseInt(data.hp)}>
                </div>
                <div class="col-md-3">
                    <label for="mana">MANA</label>
                </div>
                <div class="col-md-7">
                    <input type="number" name="mana" id="form-mana" value=${parseInt(data.mana)}>
                </div>
                <div class="col-md-3">
                    <label for="level">LEVEL</label>
                </div>
                <div class="col-md-7">
                    <select name="level" id="form-level">
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label for="attackType">AttackType</label>
                </div>
                <div class="col-md-7">
                    <select name="attackType" id="form-attackType">
                        <option value="Melee">Melee</option>
                        <option value="Ranged">Ranged</option>
                    </select>
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
           
                <div class="col-md-6" style="text-align : center;">
                    <input type="submit" value="submit">
                </div>
        `;
                var listReady = `<form id="fetch-update-frm-her" class="row">${FormCreated}</form>`;
                document.getElementById("breed-listready-content-Her").innerHTML = listReady;
                document.getElementById("fetch-update-frm-her").addEventListener("submit", fetchUpdateHero);
            });

    }

    function fetchUpdateHero(event) {

        let stringurl = window.location.href;
        let idBreed = stringurl.split('=')[1].split('&')[0]
        let idHero = stringurl.split('=')[2];
        var url = baseUrl + idBreed + "/Heroes/" + idHero;
        event.preventDefault();

        var data = {
            name: event.currentTarget.name.value,
            hp: parseInt(event.currentTarget.hp.value),
            mana: parseInt(event.currentTarget.mana.value),
            level: parseInt(event.currentTarget.level.value),
            attackType: event.currentTarget.attackType.value,
            difficulty: event.currentTarget.difficulty.value
        }
        fetch(url, {
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            },
            method: 'PUT',
            body: JSON.stringify(data)
        }).then((response) => {

            if (response.status === 200) {
                console.log("Hero updated successfuly");
            } else {
                response.text().then((data) => {

                    console.log(data);
                });
            }
        }).catch((response) => {
            console.log(data);
        });
    }

    function fetchGetSingleHero() {
        let stringurl = window.location.href;
        let idBreed = stringurl.split('=')[1].split('&')[0]
        let idHero = stringurl.split('=')[2];
        var url = baseUrl + idBreed + "/Heroes/" + idHero;
        var abilitiesperHero = "";
        
        fetch(url, {
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
                var SingleBreedHtmlMapped =
                    `
                    <h2 class="text-center Character-title">${data.name}</h2>
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
                                                    <td>Hp</td>
                                                    <td>${parseInt(data.hp)}</td>
                                                </tr>
                                                <tr>
                                                    <td>Mana</td>
                                                    <td>${parseInt(data.mana)}</td>
                                                </tr>
                                                <tr>
                                                    <td>Level</td>
                                                    <td>${parseInt(data.level)}</td>
                                                </tr>
                                                <tr>
                                                    <td>Attack Type</td>
                                                    <td>${data.attackType}</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>Difficulty</td>
                                                    <td>${data.difficulty}</td>
                                                </tr>
                                            </tbody>
                                        </div>
                                    </table>
                                </section>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <img width="500px" height="700px" style="margin-bottom: 20px;" src="images/${data.name === "Archmage" | data.name === "Blademaster" | data.name === "BloodMage" | data.name === "CryptLord" | data.name === "DeathKnight" | data.name === "DemonHunter" | data.name === "DreadLord" | data.name === "FarSeer" | data.name === "KeeperOfTheGrove" | data.name === "Lich" | data.name === "MountainKing" | data.name === "PriestessOfTheMoon" | data.name === "ShadowHunter" | data.name === "TaurenChieftain" | data.name === "Warden" | data.name === "Paladin" ? data.name : "Default"}.jpg" alt="${data.name}">
                        </div>
                    </section>
            `;
                
                data.skills.forEach(element => {
                    abilitiesperHero = abilitiesperHero + `
                    <div class="col-md-6">
                        <a href="../abilities/abilitie.html?BreedId=${data.breedId}&HeroId=${element.heroId}&id=${element.id}">
                            <div class="image text-center">
                                <img width="200px" height="200px" src="${element.link}" alt="${element.name}">
                            </div>
                            <div class="image-title-others text-center ">
                                <span>${element.name}</span>
                            </div>
                        </a>
                    </div>
                `;
                });
                var AbilitiesperHero = abilitiesperHero + `
                <div class="col-md-6">
                    <a href="../Abilities/CreateAbilitie.html?BreedId=${data.breedId}&HeroId=${data.id}">
                        <div class="image text-center">
                            <img width="200px" height="200px" src="images/Add.png" alt="addAbilitie">
                        </div>
                        <div class="image-title-others text-center ">
                            <span>Add New Abilitie</span>
                        </div>
                    </a>
                </div>
                `;
                
                document.getElementById("Singlebreed-list-content-Her").innerHTML = SingleBreedHtmlMapped;
                document.getElementById("CreateAbilitieDoc").innerHTML = AbilitiesperHero;
            });

    }

    if (document.getElementById("HeroesCards")) {
        document.getElementById("HeroesCards").addEventListener("load", fetchHeroes());
    }
    if (document.getElementById("fetch-frm-Her") != null) {
        document.getElementById("fetch-frm-Her").addEventListener("submit", fetchPostHero);
    }


    if (document.getElementById("Singlebreed-list-content-Her") != null) {
        document.getElementById("Singlebreed-list-content-Her").addEventListener("load", fetchGetSingleHero());
    }

    if (document.getElementById("fetch-delete-Btn-Her") != null) {
        document.getElementById("fetch-delete-Btn-Her").addEventListener("click", fetchDeleteHero);
    }
    if (document.getElementById("fetch-CreateForm-Btn-Her") != null) {
        document.getElementById("fetch-CreateForm-Btn-Her").addEventListener("click", fetchCreateFromHero);
    }
});