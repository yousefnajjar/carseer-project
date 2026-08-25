(() => {
    const input = document.getElementById("make-search");
    const makeId = document.getElementById("make-id");
    const makeName = document.getElementById("make-name");
    const results = document.getElementById("make-results");
    const form = document.getElementById("lookup-form");

    if (!input || !makeId || !makeName || !results) {
        return;
    }

    let timer = 0;

    const hideResults = () => {
        results.hidden = true;
        results.innerHTML = "";
        input.setAttribute("aria-expanded", "false");
    };

    const selectMake = (id, name) => {
        makeId.value = id;
        makeName.value = name;
        input.value = name;
        hideResults();
    };

    const render = (makes) => {
        results.innerHTML = "";

        if (!makes.length) {
            const empty = document.createElement("li");
            empty.className = "empty";
            empty.textContent = "No matching makes";
            results.appendChild(empty);
            results.hidden = false;
            input.setAttribute("aria-expanded", "true");
            return;
        }

        makes.forEach((make) => {
            const item = document.createElement("li");
            const button = document.createElement("button");
            button.type = "button";
            button.textContent = make.name;
            button.addEventListener("click", () => selectMake(make.id, make.name));
            item.appendChild(button);
            results.appendChild(item);
        });

        results.hidden = false;
        input.setAttribute("aria-expanded", "true");
    };

    const search = async (term) => {
        if (term.length < 2) {
            hideResults();
            return;
        }

        const response = await fetch(`/api/makes?search=${encodeURIComponent(term)}`);
        if (!response.ok) {
            hideResults();
            return;
        }

        render(await response.json());
    };

    input.addEventListener("input", () => {
        makeId.value = "";
        makeName.value = input.value;
        window.clearTimeout(timer);
        timer = window.setTimeout(() => {
            search(input.value.trim()).catch(() => hideResults());
        }, 200);
    });

    document.addEventListener("click", (event) => {
        if (!event.target.closest(".make-field")) {
            hideResults();
        }
    });

    form?.addEventListener("submit", (event) => {
        if (!makeId.value) {
            event.preventDefault();
            input.focus();
        }
    });
})();
