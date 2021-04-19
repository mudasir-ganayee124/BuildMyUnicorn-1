

function copyToClipboard(element) {

    var copyText = document.getElementById(element);
    copyText.type = 'text';
    copyText.select();
    document.execCommand("copy");
    copyText.type = 'hidden';
    toastMessage("Copy", "success", "Affiliate link copyied to clipboard");

}
new Chart(document.getElementById("chart2"),
    {
        "type": "doughnut",
        "data": {
            "labels": ["Visits", "Referrals"],
            "datasets": [{
                "label": "My First Dataset",
                "data": [300, 50],
                "backgroundColor": ["rgb(255, 99, 132)", "rgb(54, 162, 235)", "rgb(255, 205, 86)"]
            }
            ]
        }
    });

function toastMessage(heading, icon, message) {
    $.toast({
        heading: heading,
        text: message,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: icon,
        hideAfter: 1500,
        stack: 18
    });
}

