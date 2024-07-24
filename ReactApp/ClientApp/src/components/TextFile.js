import React, { useState } from 'react';

const FileUpload = () => {
  const [file, setFile] = useState(null); // State to store the selected file
  const [loading, setLoading] = useState(false); // State to manage loading status
  const [error, setError] = useState(''); // State to manage errors
  const [fileData, setFileData] = useState(null); // State to store file data after upload

  // Function to handle file upload
  const uploadFile = async (file) => {
    setLoading(true); // Set loading to true
    setError(''); // Clear any previous error messages

    try {
      // Create FormData object to send file
      const formData = new FormData();
      formData.append('file', file);

      // Perform the file upload request
      const response = await fetch('https://localhost:7246/api/Upload', {
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        throw new Error('Network response was not ok.');
      }
      
      const data = await response.json();
      console.log(data.fileName, data.fileContent);
      setFileData(data); // Store the file data
    
    } catch (err) {
      setError('Error uploading file.'); // Set error message
    } finally {
      setLoading(false); // Set loading to false
    }
  };

  // Event handler for file input change
  const handleFileChange = (event) => {
    const selectedFile = event.target.files[0];
    if (selectedFile) {
      setFile(selectedFile); // Store the selected file
      uploadFile(selectedFile); // Call the upload function
    }
  };

  return (
    <div>
      <h1>Upload a Text File</h1>
      <input type="file" onChange={handleFileChange} />
      <button onClick={() => file && uploadFile(file)} disabled={loading}>
        {loading ? 'Uploading...' : 'Upload'}
      </button>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {fileData && (
        <div>
          <h2>File Information:</h2>
          <p><strong>File Name:</strong> {fileData.fileName}</p>
          <h3>File Content:</h3>
          <pre>{fileData.fileContent}</pre>
        </div>
      )}
    </div>
  );
};

export { FileUpload };