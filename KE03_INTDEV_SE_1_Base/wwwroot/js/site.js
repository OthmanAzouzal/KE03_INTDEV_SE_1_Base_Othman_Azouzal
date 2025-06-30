document.getElementById('logoutBtn').addEventListener('click', function (event) {
    event.preventDefault();  // Voorkomt de standaard actie van de link

    if (confirm("Weet je zeker dat je wilt uitloggen?")) {
        // Als de gebruiker bevestigt, doorverwijzen naar de logout actie
        window.location.href = '/Account/Logout'; // Zorg ervoor dat dit de juiste route is
    }
});
