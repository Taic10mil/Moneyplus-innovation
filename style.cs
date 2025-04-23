<!-- style.css -->body { margin: 0; font-family: 'Segoe UI', sans-serif; background-color: #0f1c2e; color: white; }

header { background-color: #122841; padding: 1em; text-align: center; font-size: 1.5em; font-weight: bold; }

.navbar { display: flex; justify-content: space-around; background-color: #1a2d44; padding: 1em 0; box-shadow: 0 4px 6px rgba(0,0,0,0.3); position: sticky; top: 0; z-index: 100; }

.navbar button { background-color: transparent; border: none; color: white; font-size: 1em; cursor: pointer; transition: 0.3s; }

.navbar button:hover { color: #00b3ff; transform: scale(1.1); }

.profile-section { display: flex; align-items: center; gap: 1em; padding: 1em; }

.profile-pic { border-radius: 50%; width: 60px; height: 60px; background-color: #2c3e50; display: flex; align-items: center; justify-content: center; font-weight: bold; }

.card { background-color: #1c2e4a; border-radius: 12px; margin: 1em; padding: 1em; transition: 0.3s ease; box-shadow: 0 2px 10px rgba(0,0,0,0.3); }

.card:hover { transform: translateY(-5px); }

button { padding: 0.5em 1.2em; border: none; border-radius: 10px; background-color: #00b3ff; color: white; font-weight: bold; cursor: pointer; transition: background 0.3s; }

button:hover { background-color: #0092cc; }

.earning { font-size: 1.3em; text-align: center; padding: 1em 0; }

.withdraw-form input, .bank-form input { width: 100%; margin: 0.5em 0; padding: 0.6em; border-radius: 8px; border: none; background-color: #2f4a6d; color: white; }

/* Floating animation */ .card { animation: float 3s ease-in-out infinite; }

@keyframes float { 0%, 100% { transform: translateY(0); } 50% { transform: translateY(-5px); } }

/* dashboard.js */ import { initializeApp } from "https://www.gstatic.com/firebasejs/11.6.0/firebase-app.js"; import { getAuth, onAuthStateChanged } from "https://www.gstatic.com/firebasejs/11.6.0/firebase-auth.js"; import { getFirestore, doc, getDoc, setDoc, updateDoc, collection, addDoc, query, where, getDocs } from "https://www.gstatic.com/firebasejs/11.6.0/firebase-firestore.js";

const firebaseConfig = { apiKey: "AIzaSyBG5Of7xoPXX0zg2NWLER0eDL_NxZu39yc", authDomain: "moneyplus-innovation-4567c.firebaseapp.com", projectId: "moneyplus-innovation-4567c", storageBucket: "moneyplus-innovation-4567c.appspot.com", messagingSenderId: "436726330737", appId: "1:436726330737:web:233d49ac11d1f625a3f2d0" };

const app = initializeApp(firebaseConfig); const auth = getAuth(app); const db = getFirestore(app);

let currentUser = null;

onAuthStateChanged(auth, async user => { if (!user) { window.location.href = "login.html"; } else { currentUser = user; await loadUserData(user.uid); } });

async function loadUserData(uid) { const userDoc = await getDoc(doc(db, "users", uid)); if (userDoc.exists()) { const data = userDoc.data(); document.querySelector(".earning-amount").textContent = Ksh${data.earnings || 0}.00; document.querySelector("#name").textContent = data.name || "No Name"; document.querySelector("#email").textContent = data.email || "-"; document.querySelector("#phone").textContent = data.phone || "-"; document.querySelector("#bank").value = data.bank || ""; } }

async function updateBankDetails() { const bank = document.querySelector("#bank").value; if (currentUser) { await updateDoc(doc(db, "users", currentUser.uid), { bank }); alert("Bank details updated"); } }

async function requestWithdrawal() { const amount = parseFloat(document.querySelector("#withdraw-amount").value); const userDoc = await getDoc(doc(db, "users", currentUser.uid)); if (!userDoc.exists()) return;

const data = userDoc.data(); const withdrawsRef = collection(db, "withdrawals");

if ((data.withdrawCount || 0) === 0 || amount >= 300) { await addDoc(withdrawsRef, { userId: currentUser.uid, amount, timestamp: Date.now(), bank: data.bank || "-" }); await updateDoc(doc(db, "users", currentUser.uid), { withdrawCount: (data.withdrawCount || 0) + 1 }); alert("Withdrawal requested successfully!"); } else { alert("Minimum withdrawal amount is KES 300 after first withdrawal."); } }

