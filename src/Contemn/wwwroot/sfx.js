let isMuted = false;
function playSfx(sfx) {
    if (isMuted) {
        return;
    }
    let audio = document.getElementById(sfx);
    audio.currentTime = 0;
    audio.play();
}
function toggleMute() {
    isMuted = !isMuted;
}