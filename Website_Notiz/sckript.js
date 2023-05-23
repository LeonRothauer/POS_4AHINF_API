const notesList = document.getElementById("notes-list");
const addBox = document.querySelector(".add-box");
const popupBox = document.querySelector(".popup-box2");
const closeButton = document.querySelector(".popup2 .content header i");
const noteTitleInput = document.getElementById("note-title");
const noteDescriptionInput = document.getElementById("note-description");
const updateNoteButton = document.getElementById("update-note-button");
let selectedNoteId = null;
let countdownInterval = null;
let isButtonVisible = false; // Flag, um zu überprüfen, ob die Buttons angezeigt werden

function clearNoteInputs() {
  noteTitleInput.value = "";
  noteDescriptionInput.value = "";
}

addBox.addEventListener("click", () => {
  popupBox.classList.add("show");
});

function fetchNotes() {
  fetch("http://localhost:3020/notizen")
    .then(response => response.json())
    .then(data => {
      notesList.innerHTML = '';
      data.forEach(note => {
        const listItem = document.createElement("li");
        listItem.setAttribute("data-id", note.id);
        listItem.innerHTML = `
          <h3>${note.titel}</h3>
          <p>${note.notiz}</p>
          <span>${note.erstelldatum}</span>
          <div class="icon-bottom-right"><i class="uil uil-minus"></i></div>
        `;
        notesList.appendChild(listItem);
      });
    })
    .catch(error => console.log(error));
}

notesList.addEventListener("click", (event) => {
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
    });

    deleteButton.addEventListener("click", () => {
      const noteId = listItem.getAttribute("data-id");
      deleteNoteFromAPI(noteId);
    });

    countdownInterval = setInterval(() => {
      listItem.classList.remove("selected");
      noteOptions.remove();
      const minusIcon = listItem.querySelector(".icon-bottom-right i");
      minusIcon.style.display = "inline-block";
      isButtonVisible = false;
      clearInterval(countdownInterval);
    }, 5000);

    const minusIcon = listItem.querySelector(".icon-bottom-right i");
    minusIcon.style.display = "none";
    isButtonVisible = true;
  }
});

function editNote() {
  const selectedNote = notesList.querySelector(".selected");

  if (selectedNote) {
    const noteId = selectedNote.getAttribute("data-id");
    const noteTitle = selectedNote.querySelector("h3").textContent;
    const noteDescription = selectedNote.querySelector("p").textContent;

    noteTitleInput.value = noteTitle;
    noteDescriptionInput.value = noteDescription;
    selectedNoteId = noteId;
    popupBox.classList.add("show");
  }
}

function updateNote() {
    console.log("eas");
  const updatedTitle = noteTitleInput.value;
  const updatedDescription = noteDescriptionInput.value;

  const updatedNote = {
    titel: updatedTitle,
    notiz: updatedDescription
  };

  fetch(`http://localhost:3020/notizen/${selectedNoteId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(updatedNote)
  })
    .then(response => response.json())
    .then(data => {
      fetchNotes();
      popupBox.classList.remove("show");
      clearNoteInputs();
    })
    .catch(error => console.log(error));
}

updateNoteButton.addEventListener("click", () => {
  updateNote();
});

function deleteNote() {
  const selectedNote = notesList.querySelector(".selected");
  const noteId = selectedNote.getAttribute("data-id");

  fetch(`http://localhost:3020/notizen/${noteId}`, {
    method: 'DELETE'
  })
    .then(response => response.json())
    .then(data => {
      fetchNotes();
    })
    .catch(error => console.log(error));
}

fetchNotes();
