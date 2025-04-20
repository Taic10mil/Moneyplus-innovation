// dashboard.js

// Check if user is logged in firebase.auth().onAuthStateChanged(function(user) { if (user) { document.querySelector(".user-email").textContent = user.email; } else { window.location.href = "login.html"; // redirect if not logged in } });

// Logout functionality document.getElementById("logoutBtn").addEventListener("click", function() { firebase.auth().signOut().then(function() { window.location.href = "login.html"; }).catch(function(error) { console.error("Logout Error:", error); }); });

// Subscription payment logic placeholder document.getElementById("subscribeBtn").addEventListener("click", function() { alert("KES 200 activation required. Payment integration coming soon."); });

// Navigation buttons document.getElementById("tasksBtn").addEventListener("click", function() { alert("Redirecting to tasks section..."); // placeholder logic });

document.getElementById("profileBtn").addEventListener("click", function() { alert("Opening profile section..."); // placeholder logic });

