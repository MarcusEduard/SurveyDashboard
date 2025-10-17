// exportHelpers.js

// Helper: trigger browser download
function triggerDownload(blob, fileName) {
    const link = document.createElement("a");
    const url = URL.createObjectURL(blob);
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
}

// Download CSV
window.downloadCSV = (csvData, fileName) => {
    const blob = new Blob([csvData], { type: "text/csv;charset=utf-8;" });
    triggerDownload(blob, fileName);
};

// Download Excel (JSON → XLSX)
window.downloadExcel = (jsonData, fileName) => {
    try {
        if (typeof XLSX === 'undefined') {
            console.error("XLSX library not loaded");
            alert("Excel export library not loaded. Please refresh the page and try again.");
            return;
        }
        
        const data = JSON.parse(jsonData);
        if (!data || data.length === 0) {
            alert("No data available for Excel export.");
            return;
        }
        
        const worksheet = XLSX.utils.json_to_sheet(data);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, "Survey Data");
        XLSX.writeFile(workbook, fileName);
    } catch (err) {
        console.error("Excel export failed:", err);
        alert("Excel export failed: " + err.message);
    }
};

// Download PDF (tabelformat med jsPDF + autoTable)
window.downloadPDF = (jsonData, fileName) => {
    try {
        if (typeof jspdf === 'undefined') {
            console.error("jsPDF library not loaded");
            alert("PDF export library not loaded. Please refresh the page and try again.");
            return;
        }
        
        const data = JSON.parse(jsonData);
        if (!data || data.length === 0) {
            alert("No data available for PDF export.");
            return;
        }

        const doc = new jspdf.jsPDF({ orientation: "landscape" });
        doc.setFontSize(16);
        doc.text("Survey Data Export", 14, 20);
        doc.setFontSize(12);
        doc.text(`Generated: ${new Date().toLocaleDateString('da-DK')}`, 14, 30);

        // Udtræk kolonnenavne
        const headers = Object.keys(data[0]);
        const rows = data.map(item => headers.map(h => {
            const value = item[h];
            return value !== null && value !== undefined ? String(value) : "";
        }));

        // Brug autoTable
        doc.autoTable({
            head: [headers],
            body: rows,
            startY: 40,
            styles: { 
                fontSize: 8, 
                cellPadding: 2,
                overflow: 'linebreak',
                cellWidth: 'wrap'
            },
            headStyles: { 
                fillColor: [28, 167, 69],
                textColor: [255, 255, 255],
                fontStyle: 'bold'
            },
            alternateRowStyles: { fillColor: [248, 249, 250] },
            columnStyles: {
                0: { cellWidth: 20 } // Survey ID column
            }
        });

        doc.save(fileName);
    } catch (err) {
        console.error("PDF export failed:", err);
        alert("PDF export failed: " + err.message);
    }
};
