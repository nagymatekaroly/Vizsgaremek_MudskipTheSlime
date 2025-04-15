window.loginUser = async function (username, password) {
    const response = await fetch("https://mudskipdb.onrender.com/api/User/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
        credentials: "include"
    });

    if (response.ok) {
        return "ok";
    } else {
        try {
            const text = await response.text();
            return text || "Ismeretlen hiba.";
        } catch {
            return "Ismeretlen hiba történt.";
        }
    }
};


window.fetchMyHighscoresByUserId = async function (userId) {
    try {
        const response = await fetch("https://mudskipdb.onrender.com/api/Highscore/my-highscores?userId=" + userId);
        if (response.ok) {
            const data = await response.json(); // valódi JSON objektum
            return JSON.stringify(data); // stringként visszaadva, amit Blazor tud deszerializálni
        } else {
            console.error("Sikertelen válasz:", response.status);
        }
    } catch (error) {
        console.error("Hiba történt:", error);
    }

    return "";
};

window.fetchLevelHighscores = async function (levelId) {
    try {
        const response = await fetch("https://mudskipdb.onrender.com/api/Highscore/" + levelId);
        if (response.ok) {
            const data = await response.json(); // JSON objektumként vesszük át
            return JSON.stringify(data);        // stringként adjuk vissza Blazor felé
        } else {
            console.error("Sikertelen válasz a level highscore-ra:", response.status);
        }
    } catch (error) {
        console.error("Hiba történt a level highscore lekérésnél:", error);
    }

    return "";
};
