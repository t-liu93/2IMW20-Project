#Time complexity analysis using polynomial regression

#Read dataset
timeComplexity <- read.csv("TimeComplexity.csv", header=TRUE)
nodes <- timeComplexity$Nodes
edges <- timeComplexity$Edges
TRPWTime <- timeComplexity$TRPWTime.s.
edgeOneHalf <- edges^1.5
edgesquare <- edges^2

#Linear fit
linearFit <- lm(TRPWTime~edges)
anova(linearFit)

#Degree 1.5 fit
oneHalfFit <- lm(TRPWTime~edges+edgeOneHalf)
anova(oneHalfFit)

#Square fit
squareFit <- lm(TRPWTime~edges+edgesquare)
anova(squareFit)
