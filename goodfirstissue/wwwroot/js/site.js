document.getElementById("fetchButton").addEventListener("click", function () {
    const language = encodeURIComponent(document.getElementById("languageInput").value);
    const sortBy = document.getElementById("sortByInput").value;
    const endpoint = `/Issues/GetGoodFirstIssues?language=${language}&sortBy=${sortBy}`;

    fetch(endpoint)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            let htmlContent = '';
            data.forEach(issue => {
                const repoName = issue.url.split('/')[4];
                htmlContent += `
<div class="issue-card card mb-3" data-url="${issue.url}" style="cursor: pointer;">
    <div class="card-body">
        <h5 class="card-title">${issue.title}</h5>
        <p class="card-text">Repository: ${repoName}</p>
    </div>
</div>`;
            });

            document.getElementById("results").innerHTML = htmlContent;

            // Handle the click event for the cards
            document.querySelectorAll('.issue-card').forEach(card => {
                card.addEventListener('click', function () {
                    const apiUrl = this.getAttribute('data-url');
                    const githubUrl = apiUrl.replace('api.', '').replace('/repos', '').replace('issues/', 'issues/');
                    window.open(githubUrl, '_blank');
                });
            });
        })
        .catch(error => {
            console.error('There was an error with the fetch operation:', error.message);
        });
});
