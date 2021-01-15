window.addEventListener('load', (event) => {
    if (!Boolean(sessionStorage.getItem("jwt"))) {
        window.location.href = "../Login.html";
    }
    const baseUrl = 'http://localhost:51740/api/Breeds/';

    function fetchPostskill(event) {
        let stringurl = window.location.href;
        let breedId = stringurl.split('=')[1].split('&')[0];
        let heroId = stringurl.split('=')[2];
        
        event.preventDefault();
        var data = {
            name: event.currentTarget.name.value,
            button: event.currentTarget.tecla.value,
            description: event.currentTarget.description.value,
            link: event.currentTarget.link.value
        }
        var url = baseUrl + breedId + "/Heroes/" + heroId+"/Skills";
        fetch(url, {
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            },
            method: 'POST',
            body: JSON.stringify(data)
        }).then((response) => {
            if (response.status === 201) {
                console.log("Skill created successfuly");
            } else {
                response.text().then((data) => {
                    console.log(data);
                });
            }
        }).catch((response) => {
            console.log(data);
        });
    }

    function fetchDeleteskill(event) {
        let stringurl = window.location.href;
        let idBreed = stringurl.split('=')[1].split('&')[0]
        let idHero = stringurl.split('=')[2].split('&')[0];
        let idskill = stringurl.split('=')[3];
        var url = baseUrl + idBreed + "/Heroes/" + idHero + "/skills/" + idskill;
        fetch(url, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            }
        });
    }

    function fetchGetSingleskill() {
        let stringurl = window.location.href;
        let idBreed = stringurl.split('=')[1].split('&')[0]
        let idHero = stringurl.split('=')[2].split('&')[0];
        let idskill = stringurl.split('=')[3];
        var url = baseUrl + idBreed + "/Heroes/" + idHero + "/skills/" + idskill;
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
                                                    <td>Boton</td>
                                                    <td>${data.button}</td>
                                                </tr>
                                                <tr>
                                                    <td>Description</td>
                                                    <td>${ data.description}</td>
                                                </tr>
                                            </tbody>
                                        </div>
                                    </table>
                                </section>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <img width="500px" height="700px" style="margin-bottom: 20px;" src="${data.link}" alt="${data.name}">
                        </div>
                    </section>
            `;
                document.getElementById("Singlebreed-list-content-Abil").innerHTML = SingleBreedHtmlMapped;
            });

    }


    if (document.getElementById("fetch-frm-Abil") != null) {
        document.getElementById("fetch-frm-Abil").addEventListener("submit", fetchPostskill);
    }

    if (document.getElementById("Singlebreed-list-content-Abil") != null) {
        document.getElementById("Singlebreed-list-content-Abil").addEventListener("load", fetchGetSingleskill());
    }

    if (document.getElementById("fetch-delete-Btn-Abil") != null) {
        document.getElementById("fetch-delete-Btn-Abil").addEventListener("click", fetchDeleteskill);
    }

});