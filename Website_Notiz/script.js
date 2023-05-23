const notesList = document.getElementById("notes-list");
const addBox = document.querySelector(".add-box");
const popupBox = document.querySelector(".popup-box");
const popupBox2 = document.querySelector(".popup-box2");
const closeButton = document.querySelector(".popup .content header i");
const closeButton2 = document.querySelector(".popup2 .content2 header i");
const noteTitleInput = document.getElementById("new-note-title");
const noteDescriptionInput = document.getElementById("new-note-description");
const addNoteButton = document.getElementById("add-note-button");
const updateNoteButton = document.getElementById("update-note-button");

let countdownInterval = null;
let isButtonVisible = false;

addBox.addEventListener("click", () => {
  popupBox.classList.add("show");
  hideMinusIcons();
});

closeButton.addEventListener("click", () => {
  popupBox.classList.remove("show");
  clearNoteInputs();
  showMinusIcons();
});

closeButton2.addEventListener("click", () => {
  popupBox2.classList.remove("show");
  clearNoteInputs();
  showMinusIcons();
});

function clearNoteInputs() {
  noteTitleInput.value = "";
  noteDescriptionInput.value = "";
}

addNoteButton.addEventListener("click", () => {
  const noteTitle = noteTitleInput.value.trim();
  const noteDescription = noteDescriptionInput.value.trim();

  if (noteTitle !== "" && noteDescription !== "") {
    addNoteToAPI(noteTitle, noteDescription);
    clearNoteInputs();
    popupBox.classList.remove("show");
    fetchNotes();
  } else {
    alert("Please fill in all fields.");
  }
});

updateNoteButton.addEventListener("click", () => {
  const updatedNoteTitle = document.getElementById("update-note-title").value.trim();
  const updatedNoteDescription = document.getElementById("update-note-description").value.trim();
  const noteId = updateNoteButton.getAttribute("data-id");

  if (updatedNoteTitle !== "" && updatedNoteDescription !== "") {
    updateNoteInAPI(noteId, updatedNoteTitle, updatedNoteDescription);
    updateNoteButton.classList.remove("show");
    addNoteButton.classList.add("show");
    clearNoteInputs();
    popupBox2.classList.remove("show");
  } else {
    alert("Please fill in all fields.");
    console.log("johan");
  }
});


function fetchNotes() {
  fetch("http://localhost:3020/notizen")
    .then(response => response.json())
    .then(data => {
      notesList.innerHTML = "";
      data.forEach(note => {
        const listItem = document.createElement("li");
        listItem.setAttribute("data-id", note.id);
        listItem.innerHTML = `
          <h3 class="note-title">${note.titel}</h3>
          <p class="note-notiz">${note.notiz}</p>
          <span class="date">${note.erstelldatum}</span>
          <div class="icon-bottom-right"><i class="uil uil-minus"></i></div>
        `;
        notesList.appendChild(listItem);
      });
    })
    .catch(error => console.log(error));
}

function addNoteToAPI(title, description) {
  const newNote = {
    titel: title,
    notiz: description,
    erstelldatum: new Date().toLocaleString()
  };

  fetch("http://localhost:3020/notiz", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(newNote)
  })
    .then(response => {
      if (response.ok) {
        fetchNotes();
      } else {
        throw new Error("Failed to add note to API.");
      }
    })
    .catch(error => console.log(error));
}

function deleteNoteFromAPI(noteId) {
  fetch(`http://localhost:3020/notiz/${noteId}`, {
    method: "DELETE"
  })
    .then(response => {
      if (response.ok) {
        fetchNotes();
      } else {
        throw new Error("Failed to delete note from API.");
      }
    })
    .catch(error => console.log(error));
}

notesList.addEventListener("click", event => {
  const target = event.target;
  if (target.classList.contains("uil-minus")) {
    const listItem = target.closest("li");
    const isSelected = listItem.classList.contains("selected");

    if (isButtonVisible || isSelected) {
      return;
    }

    listItem.classList.add("selected");
    const noteOptions = document.createElement("div");
    noteOptions.classList.add("note-options");
    noteOptions.innerHTML = `
      <button class="edit-button">Edit</button>
      <button class="delete-button">Delete</button>
    `;
    listItem.appendChild(noteOptions);
    noteOptions.style.display = "block";

    const editButton = noteOptions.querySelector(".edit-button");
    const deleteButton = noteOptions.querySelector(".delete-button");

    editButton.addEventListener("click", () => {
      editNote();
      hideMinusIcons();
    });

    deleteButton.addEventListener("click", () => {
      const noteId = listItem.getAttribute("data-id");
      deleteNoteFromAPI(noteId);
    });

    countdownInterval = setInterval(() => {
      listItem.classList.remove("selected");
      noteOptions.remove();
      if(popupBox2.classList.contains("show"))
      {
        const minusIcon = listItem.querySelector(".icon-bottom-right i");
        minusIcon.style.display = "none";
      }
      else{
        const minusIcon = listItem.querySelector(".icon-bottom-right i");
        minusIcon.style.display = "inline-block";
      }
      isButtonVisible = false;
      clearInterval(countdownInterval);
    }, 5000);

    const minusIcon = listItem.querySelector(".icon-bottom-right i");
    minusIcon.style.display = "none";
    isButtonVisible = true;
  }
});

function editNote() {
  const listItem = document.querySelector("li.selected");
  const noteId = listItem.getAttribute("data-id");
  const noteTitle = listItem.querySelector("h3").innerText;
 const noteDescription = listItem.querySelector("p").innerText;
  console.log(noteTitle);
  const noteTitleInput = document.getElementById("update-note-title");
  const noteDescriptionInput = document.getElementById("update-note-description");
 
  noteTitleInput.value = noteTitle;
  noteDescriptionInput.value = noteDescription;
  updateNoteButton.classList.add("show");
  addNoteButton.classList.remove("show");

  updateNoteButton.setAttribute("data-id", noteId);

  popupBox2.classList.add("show");
  showMinusIcons();
}

function updateNoteInAPI(noteId, title, description) {
  const updatedNote = {
    id: noteId,
    titel: title,
    notiz: description
  };

  fetch(`http://localhost:3020/notiz/${noteId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(updatedNote)
  })
    .then(response => {
      if (response.ok) {
        return response.json();
      } else {
        throw new Error("Failed to update note in API.");
      }
    })
    .then(data => {
      const listItem = document.querySelector(`li[data-id="${noteId}"]`);
      listItem.querySelector("h3").textContent = data.titel;
      listItem.querySelector("p").textContent = data.notiz;
      listItem.querySelector("span").textContent = data.erstelldatum;

      clearNoteInputs();
      popupBox2.classList.remove("show");
    })
    .catch(error => console.log(error));
}

function hideMinusIcons() {
  const minusIcons = document.querySelectorAll(".icon-bottom-right i");
  minusIcons.forEach(icon => {
    icon.style.display = "none";
  });
}

function showMinusIcons() {
  const minusIcons = document.querySelectorAll(".icon-bottom-right i");
  minusIcons.forEach(icon => {
    icon.style.display = "inline-block";
  });
}

fetchNotes();
