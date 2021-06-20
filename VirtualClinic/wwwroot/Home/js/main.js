
let x1 = 350;
let x2 = -300;



gsap.registerPlugin(ScrollTrigger);
gsap.to('#left', {
    x: x1,
    duration: 7,
    scrollTrigger: {
        trigger: ".services",
        start: "top 90%",
        end: "top 10%",
        scrub: 3,
    }
});
gsap.to('#right', {
    x: x2,
    duration: 7,
    scrollTrigger: {
        trigger: ".services",
        start: "top 90%",
        end: "top 10%",
        scrub: 3,
    }
});

gsap.to('.section-title', {
    y: 250,
    duration: 7,
    scrollTrigger: {
        trigger: "#services",
        start: "top 90%",
        end: "top 10%",
        scrub: 3,
    }
});


