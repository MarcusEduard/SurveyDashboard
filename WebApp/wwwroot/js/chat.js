// Chat functionality JavaScript

window.scrollChatToBottom = () => {
    const chatContainer = document.getElementById('chatMessages');
    if (chatContainer) {
        chatContainer.scrollTop = chatContainer.scrollHeight;
    }
    
    // Also try the enhanced chat container
    const enhancedChatContainer = document.querySelector('.chat-messages-enhanced');
    if (enhancedChatContainer) {
        enhancedChatContainer.scrollTop = enhancedChatContainer.scrollHeight;
    }
};

// Auto-resize textarea based on content
window.autoResizeTextarea = (element) => {
    element.style.height = 'auto';
    element.style.height = (element.scrollHeight) + 'px';
};

// Copy text to clipboard with fallback
window.copyToClipboard = async (text) => {
    try {
        if (navigator.clipboard && window.isSecureContext) {
            await navigator.clipboard.writeText(text);
            return true;
        } else {
            // Fallback for older browsers
            const textArea = document.createElement('textarea');
            textArea.value = text;
            textArea.style.position = 'fixed';
            textArea.style.left = '-999999px';
            textArea.style.top = '-999999px';
            document.body.appendChild(textArea);
            textArea.focus();
            textArea.select();
            const result = document.execCommand('copy');
            textArea.remove();
            return result;
        }
    } catch (err) {
        console.error('Failed to copy text: ', err);
        return false;
    }
};

// Initialize chat functionality when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    // Add event listeners for textarea auto-resize
    document.addEventListener('input', function(e) {
        if (e.target.classList.contains('chat-input-enhanced')) {
            autoResizeTextarea(e.target);
        }
    });
    
    // Scroll to bottom when chat is loaded
    setTimeout(() => {
        scrollChatToBottom();
    }, 100);
});