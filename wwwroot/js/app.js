const apiUrlUsers = 'https://localhost:7144/api/UserProfile';  // Your API endpoint for users
const apiUrlPosts = 'https://localhost:7144/api/Post';  // Your API endpoint for posts
let currentUserID = null;

document.addEventListener("DOMContentLoaded", function () {
    fetchUserProfiles();

    // Handle form submission for adding a new user profile
    document.getElementById("addUserForm").addEventListener("submit", function (event) {
        event.preventDefault();

        const firstName = document.getElementById("firstName").value;
        const lastName = document.getElementById("lastName").value;
        const email = document.getElementById("email").value;
        const dob = document.getElementById("dob").value;

        const newUser = { firstName, lastName, email, dateOfBirth: dob };

        fetch(apiUrlUsers, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newUser)
        })
            .then(response => response.json())
            .then(data => {
                alert('User added successfully');
                fetchUserProfiles();  // Refresh the list of profiles
                document.getElementById("addUserForm").reset(); // Clear form
            })
            .catch(error => console.error('Error adding user:', error));
    });
});

// Fetch and display all user profiles
function fetchUserProfiles() {
    fetch(apiUrlUsers)
        .then(response => response.json())
        .then(users => {
            const userProfilesList = document.getElementById("userProfilesList");
            userProfilesList.innerHTML = ''; // Clear the list first

            users.forEach(user => {
                const userProfileDiv = document.createElement("div");
                userProfileDiv.classList.add("profile");
                userProfileDiv.innerHTML = `
                    <p><strong>Name:</strong> ${user.firstName} ${user.lastName}</p>
                    <p><strong>Email:</strong> ${user.email}</p>
                    <p><strong>Date of Birth:</strong> ${new Date(user.dateOfBirth).toLocaleDateString()}</p>
                    <button class="view-button" onclick="viewUserProfile(${user.userID})">View Profile</button>
                    <button class="edit-button" onclick="editUserProfile(${user.userID})">Edit</button>
                    <button onclick="deleteUserProfile(${user.userID})">Delete</button>
                `;
                userProfilesList.appendChild(userProfileDiv);
            });
        })
        .catch(error => console.error('Error fetching user profiles:', error));
}

// View a user profile along with posts
function viewUserProfile(userID) {
    currentUserID = userID;

    fetch(`${apiUrlUsers}/${userID}`)
        .then(response => response.json())
        .then(user => {
            document.getElementById("userProfileName").innerText = user.firstName + " " + user.lastName;
            document.getElementById("userProfileEmail").innerText = user.email;
            document.getElementById("userProfileDob").innerText = new Date(user.dateOfBirth).toLocaleDateString();
        })
        .catch(error => console.error('Error fetching user profile:', error));

    fetchPostsForUser(userID);
    document.getElementById("userProfileForm").style.display = 'none';
    document.getElementById("userProfilesList").style.display = 'none';
    document.getElementById("userProfileDetails").style.display = 'block';
}

// Fetch and display posts for the selected user
function fetchPostsForUser(userID) {
    fetch(`${apiUrlPosts}/user/${userID}`)
        .then(response => response.json())
        .then(posts => {
            const userPostsList = document.getElementById("userPostsList");
            userPostsList.innerHTML = '';  // Clear existing posts

            if (posts.length === 0) {
                userPostsList.innerHTML = '<p>No posts available.</p>';
            }

            posts.forEach(post => {
                const postDiv = document.createElement("div");
                postDiv.classList.add("post");
                postDiv.innerHTML = `
                    <h4>${post.title}</h4>
                    <p>${post.content}</p>
                    <button onclick="editPost(${post.postID})">Edit</button>
                    <button onclick="deletePost(${post.postID})">Delete</button>
                `;
                userPostsList.appendChild(postDiv);
            });
        })
        .catch(error => console.error('Error fetching posts for user:', error));
}

// Edit a post
function editPost(postID) {
    const newTitle = prompt("Enter new title:");
    const newContent = prompt("Enter new content:");

    const updatedPost = { title: newTitle, content: newContent, userID: currentUserID };

    fetch(`${apiUrlPosts}/${postID}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedPost)
    })
        .then(response => response.json())
        .then(data => {
            alert('Post updated successfully');
            fetchPostsForUser(currentUserID);  // Refresh posts list
        })
        .catch(error => console.error('Error updating post:', error));
}

// Delete a post
function deletePost(postID) {
    fetch(`${apiUrlPosts}/${postID}`, { method: 'DELETE' })
        .then(response => response.json())
        .then(data => {
            alert('Post deleted successfully');
            fetchPostsForUser(currentUserID);  // Refresh posts list
        })
        .catch(error => console.error('Error deleting post:', error));
}

// Add a new post
document.getElementById("postForm").addEventListener("submit", function (event) {
    event.preventDefault();

    const title = document.getElementById("postTitle").value;
    const content = document.getElementById("postContent").value;
    const newPost = { title, content, userID: currentUserID };

    fetch(apiUrlPosts, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(newPost)
    })
        .then(response => response.json())
        .then(data => {
            alert('Post added successfully');
            fetchPostsForUser(currentUserID);  // Refresh the posts list
            document.getElementById("postForm").reset(); // Clear form
        })
        .catch(error => console.error('Error adding post:', error));
});

// Cancel Edit User
function cancelEdit() {
    document.getElementById("editUserForm").style.display = 'none';
    document.getElementById("userProfileForm").style.display = 'block';  // Show user list
}
